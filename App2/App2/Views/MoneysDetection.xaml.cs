using CustomVisionDemo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneysDetection : ContentPage
    {
        private const string CleDePrediction = "d75572c29a944f04b7d18dba968aa090";

        private const string UrlDePrediction = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/d5c4358e-d0ed-4762-a9e7-f4be0c4e1bb1/detect/iterations/Blind/image";

        private PredictionResponse results;
        public MoneysDetection()
        {
            InitializeComponent();
        }
        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                
                var stream = await photo.OpenReadAsync();
                PhotoImage.Source = ImageSource.FromStream(() => stream);

                RequestPrediction(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "ok");
            }
        }

        private async void Gallery_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                var stream = await photo.OpenReadAsync();
                PhotoImage.Source = ImageSource.FromStream(() => stream);

                RequestPrediction(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "ok");
            }
        }


        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        public async void RequestPrediction(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", CleDePrediction);

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(UrlDePrediction, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                DeserialiseResults(contentString);
                UpdateScreen();
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {

            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public void DeserialiseResults(string json)
        {
            Console.WriteLine("le fichier json : " + json);
            results = JsonConvert.DeserializeObject<PredictionResponse>(json);


            for (var i = 0; i < results.Predictions.Count; i++)
            {
                Console.WriteLine("TagName: " + results.Predictions[i].TagName);
            }

        }


        void UpdateScreen()
        {

            if (results.Predictions.Count > 0)
            {
                PhotoImage.IsVisible = true;
                LesTags.IsVisible = true;
                TagN1.Text = results.Predictions[0].TagName + " " + results.Predictions[0].TagName.ToString();
                TagN2.Text = results.Predictions[1].TagName + " " + results.Predictions[1].TagName.ToString();
                Console.WriteLine("TagNameeeeeeeeeeee: " + results.Predictions[0].TagName);

            }

        }


        public void InitialiseContent()
        {

            PhotoImage.IsVisible = false;
            LesTags.IsVisible = false;


        }
    }
}
