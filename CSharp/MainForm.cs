using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.Encoders;
using Vintasoft.WinTwain;

namespace TwainHighPerformanceDemo
{
    public partial class MainForm : Form
    {

        #region Classes

        /// <summary>
        /// Contains information about the image, which must be displayed in image viewer.
        /// </summary>
        class ImagePreviewingInfo
        {

            internal ImagePreviewingInfo(AcquiredImage image, int imageIndex, string imageFileName)
            {
                _image = image;
                _imageIndex = imageIndex;
                _imageFileName = imageFileName;
            }



            AcquiredImage _image;
            /// <summary>
            /// The image, which is acquired from scanner.
            /// </summary>
            internal AcquiredImage Image
            {
                get { return _image; }
            }

            int _imageIndex;
            /// <summary>
            /// The index of acquired image.
            /// </summary>
            internal int ImageIndex
            {
                get { return _imageIndex; }
            }

            string _imageFileName;
            /// <summary>
            /// The name of file, where image must be saved.
            /// </summary>
            internal string ImageFileName
            {
                get { return _imageFileName; }
            }

        }

        /// <summary>
        /// Contains information about the image, which must be saved to a file.
        /// </summary>
        class ImageSavingInfo
        {

            internal ImageSavingInfo(VintasoftImage image, int imageIndex, string imageFileName)
            {
                _image = image;
                _imageIndex = imageIndex;
                _imageFileName = imageFileName;
            }



            VintasoftImage _image;
            /// <summary>
            /// The image, which must be saved to a file.
            /// </summary>
            internal VintasoftImage Image
            {
                get { return _image; }
            }

            int _imageIndex;
            /// <summary>
            /// The index of acquired image.
            /// </summary>
            internal int ImageIndex
            {
                get { return _imageIndex; }
            }

            string _imageFileName;
            /// <summary>
            /// The name of file, where image must be saved.
            /// </summary>
            internal string ImageFileName
            {
                get { return _imageFileName; }
            }

        }

        /// <summary>
        /// Contains information about the encoded image, which is stored in memory and must be added to a multipage image file.
        /// </summary>
        class ImageEncodingInfo
        {

            internal ImageEncodingInfo(VintasoftImage encodedImage, string imageFileName)
            {
                _encodedImage = encodedImage;
                _imageFileName = imageFileName;
            }



            VintasoftImage _encodedImage;
            /// <summary>
            /// The encoded image.
            /// </summary>
            internal VintasoftImage EncodedImage
            {
                get { return _encodedImage; }
            }

            string _imageFileName;
            /// <summary>
            /// The name of file, where image must be saved.
            /// </summary>
            internal string ImageFileName
            {
                get { return _imageFileName; }
            }

        }

        #endregion



        #region Fields

        #region Image scanning

        /// <summary>
        /// TWAIN device manager.
        /// </summary>
        DeviceManager _deviceManager;

        /// <summary>
        /// Current device.
        /// </summary>
        Device _currentDevice;

        /// <summary>
        /// Count of acquired images.
        /// </summary>
        int _acquiredImageCount;

        /// <summary>
        /// Indicates that the image scanning is working.
        /// </summary>
        bool _isImageScanningWorking;

        /// <summary>
        /// Maximum count of images in the saving quieue.
        /// </summary>
        int _maxImagesInSavingQueue = 20;

        #endregion


        #region Image previewing

        /// <summary>
        /// Queue of acquired images, which must be previewed in image viewer.
        /// </summary>
        Queue<ImagePreviewingInfo> _acquiredImagesForPreview = new Queue<ImagePreviewingInfo>();
        /// <summary>
        /// Indicates that only the last image from queue must be shown.
        /// </summary>
        bool _canSkipPreviewImages = true;

        #endregion


        #region Image saving

        /// <summary>
        /// The name of directory, where acquired images must be saved.
        /// </summary>
        string _directoryForImages = "Images";
        /// <summary>
        /// The extension of file, where acquired images msu tbe saved.
        /// </summary>
        string _fileExtension = "TIFF";

        /// <summary>
        /// Indicates that acquired images must be saved to a multipage image file.
        /// </summary>
        bool _saveImagesInMultiPageFile = true;
        /// <summary>
        /// Indicates that multiple threads must be used for saving acquired images to a multipage image file.
        /// </summary>
        bool _useMultipleThreadsForSavingImagesToMultiPageFile = true;
        /// <summary>
        /// Maximum count of threads, which can be used for saving of page image.
        /// </summary>
        int _maxPageSavingThreadCount;

        /// <summary>
        /// Queue of acquired images, which must be saved into file.
        /// </summary>
        Queue<ImageSavingInfo> _acquiredImagesForSaving = new Queue<ImageSavingInfo>();

        /// <summary>
        /// Dictionary that contains encoded images, which must be saved into multipage image file.
        /// </summary>
        Dictionary<int, ImageEncodingInfo> _encodedImagesForSaving = new Dictionary<int, ImageEncodingInfo>();

        /// <summary>
        /// Count of saved images.
        /// </summary>
        int _savedImageCount;

        #endregion


        /// <summary>
        /// Indicates that the form is closing.
        /// </summary>
        bool _isFormClosing;

        #endregion



        #region Constructors

        public MainForm()
        {
            Vintasoft.Twain.TwainGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");

            try
            {
                InitializeComponent();
            }
            catch (TypeInitializationException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException is LicenseException)
                    {
                        MessageBox.Show(ex.InnerException.InnerException.Message);
                    }
                }
                throw ex;
            }

            imageFileFormatComboBox.SelectedIndex = 0;
            savingThreadCountNumericUpDown.Value = Environment.ProcessorCount;

            // create TWAIN device manager
            _deviceManager = new DeviceManager(this, this.Handle);
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// The "Acquire" button is clicked.
        /// </summary>
        private void acquireImagesButton_Click(object sender, EventArgs e)
        {
            acquireImagesButton.Enabled = false;

            GetImageScanningParamsFromUI();
            GetImagePreviewingParametersFromUI();
            GetImageSavingParamsFromUI();

            if (!StartImageScanning(showDeviceSelectionDialogCheckBox.Checked, showDeviceUiCheckBox.Checked))
                acquireImagesButton.Enabled = true;
        }

        /// <summary>
        /// Gets the image scanning parameters from UI.
        /// </summary>
        private void GetImageScanningParamsFromUI()
        {
            _maxImagesInSavingQueue = (int)maximumImageCountInSavingQueueNumericUpDown.Value;
        }

        /// <summary>
        /// Gets the image previewing parameters from UI.
        /// </summary>
        private void GetImagePreviewingParametersFromUI()
        {
            _canSkipPreviewImages = canSkipPreviewImagesCheckBox.Checked;
        }

        /// <summary>
        /// Gets the image saving parameters from UI.
        /// </summary>
        private void GetImageSavingParamsFromUI()
        {
            _fileExtension = imageFileFormatComboBox.SelectedItem.ToString();
            _saveImagesInMultiPageFile = saveImagesToMultipageFileCheckBox.Checked;
            _useMultipleThreadsForSavingImagesToMultiPageFile = useMultipleThreadsForSavingImagesToMultiPageFileCheckBox.Checked;
            _maxPageSavingThreadCount = (int)savingThreadCountNumericUpDown.Value;
        }

        /// <summary>
        /// Enables the AcquireImages button.
        /// </summary>
        private void EnableAcquireImagesButton()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EnableAcquireImagesButtonDelegate(EnableAcquireImagesButton));
            }
            else
            {
                _isImageScanningWorking = false;

                acquireImagesButton.Enabled = true;
            }
        }

        /// <summary>
        /// Main form is closing.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currentDevice != null)
            {
                // if image is acquiring
                if (_currentDevice.State > DeviceState.Enabled)
                {
                    // cancel image acquisition
                    _currentDevice.CancelTransfer();
                    // specify that form must be closed when image acquisition is canceled
                    _isFormClosing = true;
                    // cancel form closing
                    e.Cancel = true;
                    return;
                }

                // unsubscribe from device events
                UnsubscribeFromDeviceEvents(_currentDevice);
                // close the device
                _currentDevice.Close();
                _currentDevice = null;
            }

            // close the device manager
            _deviceManager.Close();
            // dispose the device manager
            _deviceManager.Dispose();

            // while image scanning, previewing or saving are working
            while (IsImageScanningPreviewingAndSavingWorking())
            {
                // wait for a while
                Thread.Sleep(10);
            }
        }

        #endregion


        #region Image scanning

        /// <summary>
        /// Starts the image scanning.
        /// </summary>
        private bool StartImageScanning(bool showDeviceSelectionDialog, bool showDeviceUI)
        {
            try
            {
                // open the device manager
                _deviceManager.Open();

                // if device selection dialog must be shown
                if (showDeviceSelectionDialog)
                    // show the device selection dialog
                    _deviceManager.ShowDefaultDeviceSelectionDialog();

                // use the default device
                _currentDevice = _deviceManager.DefaultDevice;
                _currentDevice.ShowUI = showDeviceUI;

                // subscribe to the device events
                SubscribeToDeviceEvents(_currentDevice);

                // if directory for image files does NOT exist
                if (!Directory.Exists(_directoryForImages))
                    // create the directory for image files
                    Directory.CreateDirectory(_directoryForImages);

                _acquiredImageCount = 0;
                _savedImageCount = 0;
                _isImageScanningWorking = true;

                // create the thread for image previewing
                CreateImagePreviewThread();
                // if acquired images must be saved to a multipage image file
                if (_saveImagesInMultiPageFile)
                {
                    // if multiple threads must be used for saving acquired images to a multipage image file
                    if (_useMultipleThreadsForSavingImagesToMultiPageFile)
                    {
                        // create the threads for image encoding
                        for (int i = 0; i < _maxPageSavingThreadCount; i++)
                            CreateThreadForEncodingImageToMemory();
                        // create the thread for adding encoded images to a multipage image file
                        CreateThreadForAddingEncodedImagesToMultiPageFile();
                    }
                    // if single thread must be used for saving acquired images to a multipage image file
                    else
                    {
                        // create the thread for image saving to a multipage image file
                        CreateThreadForSavingImageToFile();
                    }
                }
                // if acquired images must be saved to the singlepage image files
                else
                {
                    // create the threads for image saving to the singlepage files
                    for (int i = 0; i < _maxPageSavingThreadCount; i++)
                        CreateThreadForSavingImageToFile();
                }


                // open the device
                _currentDevice.Open();

                // get the XferCount capability
                DeviceCapability deviceXferCount = _currentDevice.Capabilities.Find(DeviceCapabilityId.XferCount);
                // if capability is found
                if (deviceXferCount != null)
                    // specify that all available images must be acquired from scanner
                    deviceXferCount.SetValue(-1);

                // get the AutoScan capability
                DeviceCapability deviceAutoScan = _currentDevice.Capabilities.Find(DeviceCapabilityId.AutoScan);
                // if capability is found
                if (deviceAutoScan != null)
                    // specify that scanner must use internal buffers and boost the performance
                    deviceAutoScan.SetValue(true);

                // start the asynchronous image acquisition from scanner
                _currentDevice.Acquire();
            }
            catch (TwainException ex)
            {
                // if device is selected
                if (_currentDevice != null)
                {
                    // if device is NOT closed
                    if (_currentDevice.State != DeviceState.Closed)
                        // close the device
                        _currentDevice.Close();
                }

                // if device manager is NOT closed
                if (_deviceManager.State != DeviceManagerState.Closed)
                    // close the device manager
                    _deviceManager.Close();

                // show dialog with error message
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Subscribe to the device events.
        /// </summary>
        private void SubscribeToDeviceEvents(Device device)
        {
            device.ImageAcquiringProgress += new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired += new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanFailed += new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            device.ScanFinished += new EventHandler(device_ScanFinished);
        }

        /// <summary>
        /// Unsubscribe from the device events.
        /// </summary>
        private void UnsubscribeFromDeviceEvents(Device device)
        {
            device.ImageAcquiringProgress -= new EventHandler<ImageAcquiringProgressEventArgs>(device_ImageAcquiringProgress);
            device.ImageAcquired -= new EventHandler<ImageAcquiredEventArgs>(device_ImageAcquired);
            device.ScanFailed -= new EventHandler<ScanFailedEventArgs>(device_ScanFailed);
            device.ScanFinished -= new EventHandler(device_ScanFinished);
        }

        /// <summary>
        /// Image acquiring progress is changed.
        /// </summary>
        private void device_ImageAcquiringProgress(object sender, ImageAcquiringProgressEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_isFormClosing)
            {
                // cancel image acquisition
                _currentDevice.CancelTransfer();
            }
        }

        /// <summary>
        /// Image is acquired.
        /// </summary>
        private void device_ImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            // image acquistion must be canceled because application's form is closing
            if (_isFormClosing)
            {
                // cancel image acquisition
                _currentDevice.CancelTransfer();
                return;
            }

            if (e.Image == null)
                return;

            int acquiredImagesForSavingCount;
            while (true)
            {
                // lock the queue for saving images
                lock (_acquiredImagesForSaving)
                {
                    // get the count of images in the queue for saving images
                    acquiredImagesForSavingCount = _acquiredImagesForSaving.Count;
                }
                // if the queue for saving images contains less than allowed image count
                if (acquiredImagesForSavingCount < _maxImagesInSavingQueue)
                    // continue scanning
                    break;

                // wait for a while for pausing the scanning
                Thread.Sleep(10);
            }

            // get name of file, where image must be saved
            string fileName = null;
            if (_saveImagesInMultiPageFile)
                fileName = string.Format("MultipageImage.{0}", _fileExtension);
            else
                fileName = string.Format("Image_{0}.{1}", _acquiredImageCount.ToString("D8"), _fileExtension);

            // create an object that coontains information about image, which must be previewed in image viewer
            ImagePreviewingInfo acquiredImageInfo = new ImagePreviewingInfo(e.Image, _acquiredImageCount, fileName);
            // lock the queue for image preview
            lock (_acquiredImagesForPreview)
            {
                // add information about acquired image into queue
                _acquiredImagesForPreview.Enqueue(acquiredImageInfo);

                // increment the count of acquired images
                _acquiredImageCount++;
            }
        }

        /// <summary>
        /// Scan is failed.
        /// </summary>
        private void device_ScanFailed(object sender, ScanFailedEventArgs e)
        {
            // show error message
            MessageBox.Show(e.ErrorString, "Scan is failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Scan is finished.
        /// </summary>
        private void device_ScanFinished(object sender, EventArgs e)
        {
            // close the device
            _currentDevice.Close();

            // unsubscribe from device events
            UnsubscribeFromDeviceEvents(_currentDevice);

            _isImageScanningWorking = false;

            // if image scanning, previewing and saving processes are finished
            if (!IsImageScanningPreviewingAndSavingWorking())
            {
                EnableAcquireImagesButton();
            }
        }

        #endregion


        #region Image previewing

        /// <summary>
        /// Creates the thread for image preview.
        /// </summary>
        private void CreateImagePreviewThread()
        {
            Thread thread = new Thread(PreviewLastAcquiredImageThreadMethod);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        /// <summary>
        /// The thread method, which previews the acquired images in the image viewer.
        /// </summary>
        private void PreviewLastAcquiredImageThreadMethod()
        {
            // while image scanning, previewing or saving are working
            while (IsImageScanningPreviewingAndSavingWorking())
            {
                // get the last acquired image for image preview
                ImagePreviewingInfo imagePreviewingInfo = GetNextAcquiredImageForPreview();
                // if there is image for preview
                if (imagePreviewingInfo != null)
                {
                    // save reference to the VintasoftImage object, which is displayed in image viewer
                    VintasoftImage previousImageViewerImage = imageViewer1.Image;

                    // create the VintasoftImage object from the AcquiredImage object
                    VintasoftImage acquireImageAsVintasoftImage = VintasoftImageGdiExtensions.Create(imagePreviewingInfo.Image.GetAsBitmap(false), true);

                    // add the acquired image to the image saving queue
                    AddAcquiredImageToImageSavingQueue(imagePreviewingInfo, acquireImageAsVintasoftImage);

                    // set the VintasoftImage object as new image in image viewer 
                    imageViewer1.Image = acquireImageAsVintasoftImage;

                    // update image info in UI
                    UpdateImageInfo();

                    // if image viewer previously displayed the image
                    if (previousImageViewerImage != null)
                        // dispose the VintasoftImage object, which was previously displayed in image viewer
                        previousImageViewerImage.Dispose();

                    // lock the queue with acquired images for preview
                    lock (_acquiredImagesForPreview)
                    {
                        // remove the acquired image from the preview queue
                        _acquiredImagesForPreview.Dequeue();
                    }
                }
                // if there is NO image for preview
                else
                    // sleep for a while
                    Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Returns the next acquired image for preview.
        /// </summary>
        /// <returns>The next acquired image for preview.</returns>
        private ImagePreviewingInfo GetNextAcquiredImageForPreview()
        {
            // lock the queue with acquired images for preview
            lock (_acquiredImagesForPreview)
            {
                // if there are NO images
                if (_acquiredImagesForPreview.Count == 0)
                    // exit
                    return null;

                // if scanner is faster than image viewer preview and NOT all acquired images must be shown
                if (_canSkipPreviewImages)
                {
                    // while queue has more than 1 image
                    while (_acquiredImagesForPreview.Count > 1)
                    {
                        // lock the queue for image saving
                        lock (_acquiredImagesForSaving)
                        {
                            ImagePreviewingInfo acquiredImageInfo = _acquiredImagesForPreview.Dequeue();

                            ImageSavingInfo savingImageInfo = new ImageSavingInfo(
                                VintasoftImageGdiExtensions.Create(acquiredImageInfo.Image.GetAsBitmap(false), true),
                                acquiredImageInfo.ImageIndex,
                                acquiredImageInfo.ImageFileName);

                            // move the first image from queue for preview into queue for image saving
                            _acquiredImagesForSaving.Enqueue(savingImageInfo);
                        }
                    }

                    // return the first image from the queue for preview
                    return _acquiredImagesForPreview.Peek();
                }
                // if all acquired images must be shown
                else
                {
                    // return the first image from queue for preview
                    return _acquiredImagesForPreview.Peek();
                }

                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Adds the acquired image to the image saving queue.
        /// </summary>
        /// <param name="imagePreviewingInfo">Information about acquired image.</param>
        /// <param name="image">Image, which must be saved.</param>
        private void AddAcquiredImageToImageSavingQueue(
            ImagePreviewingInfo imagePreviewingInfo,
            VintasoftImage image)
        {
            // dispose the AcquiredImage object
            imagePreviewingInfo.Image.Dispose();

            // create information about image, which must be saved
            ImageSavingInfo savingImageInfo = new ImageSavingInfo(
                (VintasoftImage)image.Clone(),
                imagePreviewingInfo.ImageIndex,
                imagePreviewingInfo.ImageFileName);
            // lock the queue for image saving
            lock (_acquiredImagesForSaving)
            {
                // add image to the queue for image saving
                _acquiredImagesForSaving.Enqueue(savingImageInfo);
            }
        }

        /// <summary>
        /// Updates information about acquired image in UI.
        /// </summary>
        private void UpdateImageInfo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateImageInfoDelegate(UpdateImageInfo));
            }
            else
            {
                imageInfoLabel.Text = string.Format("Image {0}", _acquiredImageCount);
            }
        }

        #endregion


        #region Image saving

        /// <summary>
        /// Creates the thread, which saves the acquired images.
        /// </summary>
        private void CreateThreadForSavingImageToFile()
        {
            Thread thread = new Thread(SaveImageToFile_ThreadMethod);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        /// <summary>
        /// The thread method, which saves the image to a file.
        /// </summary>
        private void SaveImageToFile_ThreadMethod()
        {
            // while image scanning, previewing or saving are working
            while (IsImageScanningPreviewingAndSavingWorking())
            {
                // get the image that must be saved
                ImageSavingInfo imageSavingInfo = GetNextImageForSaving();
                if (imageSavingInfo != null)
                {
                    try
                    {
                        // get path to the file
                        string filePath = Path.Combine(_directoryForImages, imageSavingInfo.ImageFileName);
                        // open the file stream
                        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            // save image to the file stream
                            SaveAcquiredImage(imageSavingInfo.Image, fs);

                            // increment count of saved images
                            _savedImageCount++;
                        }
                    }
                    finally
                    {
                        // dispose the image
                        imageSavingInfo.Image.Dispose();
                    }
                }

                Thread.Sleep(10);
            }

            EnableAcquireImagesButton();
        }

        /// <summary>
        /// Creates the thread, which encodes an image to a memory.
        /// </summary>
        private void CreateThreadForEncodingImageToMemory()
        {
            Thread thread = new Thread(EncodeImageToMemory_ThreadMethod);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        /// <summary>
        /// The thread method, which encodes image to a memory.
        /// </summary>
        private void EncodeImageToMemory_ThreadMethod()
        {
            // while image scanning, previewing or saving are working
            while (IsImageScanningPreviewingAndSavingWorking())
            {
                // get the image that must be saved
                ImageSavingInfo imageSavingInfo = GetNextImageForSaving();
                if (imageSavingInfo != null)
                {
                    try
                    {
                        MemoryStream mem = new MemoryStream();
                        // save image to the memory stream
                        SaveAcquiredImage(imageSavingInfo.Image, mem);

                        mem.Position = 0;
                        // create an object that contains information about the encoded image
                        ImageEncodingInfo encodedAcquiredImageInfo = new ImageEncodingInfo(
                            new VintasoftImage(mem, true),
                            imageSavingInfo.ImageFileName);

                        lock (_encodedImagesForSaving)
                        {
                            // save information about encoded image for adding image to multipage image file later
                            _encodedImagesForSaving.Add(imageSavingInfo.ImageIndex, encodedAcquiredImageInfo);
                        }
                    }
                    finally
                    {
                        // dispose the image
                        imageSavingInfo.Image.Dispose();
                    }
                }

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Creates the thread, which adds the encoded images to a multipage image file.
        /// </summary>
        private void CreateThreadForAddingEncodedImagesToMultiPageFile()
        {
            Thread thread = new Thread(AddEncodedImageToMultiPageFile_ThreadMethod);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        /// <summary>
        /// The thread method, which adds the encoded images to a multipage image file.
        /// </summary>
        private void AddEncodedImageToMultiPageFile_ThreadMethod()
        {
            // while image scanning, previewing or saving are working
            while (IsImageScanningPreviewingAndSavingWorking())
            {
                // get the encoded image for next page
                ImageEncodingInfo encodedAcquiredImageInfo = GetNextEncodedImageForSaving(_savedImageCount);
                // if image is ready for adding to a multipage image file
                if (encodedAcquiredImageInfo != null)
                {
                    // get path to a multipage image file
                    string filePath = Path.Combine(_directoryForImages, encodedAcquiredImageInfo.ImageFileName);

                    // get the image encoder
                    using (EncoderBase imageEncoder = GetImageEncoder())
                    {
                        // save the encoded image to a multipage image file
                        encodedAcquiredImageInfo.EncodedImage.Save(filePath, imageEncoder);
                    }
                    
                    // dispose the encoded image
                    encodedAcquiredImageInfo.EncodedImage.Dispose();

                    // increment count of saved images
                    _savedImageCount++;
                }

                Thread.Sleep(10);
            }

            EnableAcquireImagesButton();
        }

        /// <summary>
        /// Returns the next image, which must be saved.
        /// </summary>
        /// <returns>The next image, which must be saved.</returns>
        private ImageSavingInfo GetNextImageForSaving()
        {
            // lock the queue for image saving
            lock (_acquiredImagesForSaving)
            {
                // if queue does not have images
                if (_acquiredImagesForSaving.Count == 0)
                    return null;

                // return the first image from queue
                return _acquiredImagesForSaving.Dequeue();
            }
        }

        /// <summary>
        /// Returns the next encoded image, which must be saved into multipage image file.
        /// </summary>
        /// <returns>The next encoded image, which must be saved into multipage image file.</returns>
        private ImageEncodingInfo GetNextEncodedImageForSaving(int pageIndex)
        {
            ImageEncodingInfo encodedAcquiredImageInfo = null;
            lock (_encodedImagesForSaving)
            {
                // get image with specified index
                if (_encodedImagesForSaving.TryGetValue(pageIndex, out encodedAcquiredImageInfo))
                {
                    _encodedImagesForSaving.Remove(pageIndex);
                }
                return encodedAcquiredImageInfo;
            }
        }

        /// <summary>
        /// Saves the acquired image to a stream.
        /// </summary>
        /// <param name="acquiredImage">The acquired image.</param>
        /// <param name="stream">The stream, where image must be saved.</param>
        private void SaveAcquiredImage(VintasoftImage vintasoftImage, Stream stream)
        {
            // get the image encoder
            EncoderBase imageEncoder = GetImageEncoder();
            // save image to the stream
            vintasoftImage.Save(stream, imageEncoder);
        }

        /// <summary>
        /// Returns the image encoder.
        /// </summary>
        /// <returns>The image encoder.</returns>
        private EncoderBase GetImageEncoder()
        {
            switch (_fileExtension.ToUpperInvariant())
            {
                case "TIFF":
                    TiffEncoder tiffEncoder = new TiffEncoder();
                    tiffEncoder.CreateNewFile = !_saveImagesInMultiPageFile;
                    tiffEncoder.Settings.UseTiles = true;
                    tiffEncoder.Settings.TileSize = new System.Drawing.Size(1024, 1024);
                    tiffEncoder.Settings.Compression = Vintasoft.Imaging.Codecs.ImageFiles.Tiff.TiffCompression.Jpeg;
                    tiffEncoder.Settings.JpegQuality = 50;
                    return tiffEncoder;

                case "PDF":
                    PdfEncoder pdfEncoder = new PdfEncoder();
                    pdfEncoder.CreateNewFile = !_saveImagesInMultiPageFile;
                    pdfEncoder.Settings.Compression = Vintasoft.Imaging.Codecs.Encoders.PdfImageCompression.Jpeg;
                    pdfEncoder.Settings.JpegQuality = 90;
                    pdfEncoder.Settings.DocumentCreationDate = DateTime.Now;
                    return pdfEncoder;

                case "JPG":
                    JpegEncoder jpegEncoder = new JpegEncoder();
                    jpegEncoder.Settings.Quality = 90;
                    return jpegEncoder;

                case "BMP":
                    return new BmpEncoder();

                case "GIF":
                    return new GifEncoder();

                case "PNG":
                    return new PngEncoder();
            }

            throw new InvalidOperationException();
        }

        #endregion


        /// <summary>
        /// Determines that image scanning, previewing or saving processes are working.
        /// </summary>
        private bool IsImageScanningPreviewingAndSavingWorking()
        {
            if (_isImageScanningWorking)
                return true;

            int acquiredImagesForPreviewCount;
            lock (_acquiredImagesForPreview)
            {
                acquiredImagesForPreviewCount = _acquiredImagesForPreview.Count;
            }
            if (acquiredImagesForPreviewCount > 0)
                return true;


            int acquiredImagesForSavingCount;
            lock (_acquiredImagesForSaving)
            {
                acquiredImagesForSavingCount = _acquiredImagesForSaving.Count;
            }
            if (acquiredImagesForSavingCount > 0)
                return true;

            if (_saveImagesInMultiPageFile && _useMultipleThreadsForSavingImagesToMultiPageFile)
            {
                int encodedImagesForSavingCount;
                lock (_encodedImagesForSaving)
                {
                    encodedImagesForSavingCount = _encodedImagesForSaving.Count;
                }
                if (encodedImagesForSavingCount > 0)
                    return true;
            }

            return false;
        }

        #endregion



        #region Delegates

        delegate void EnableAcquireImagesButtonDelegate();
        delegate void UpdateImageInfoDelegate();

        #endregion

    }
}
