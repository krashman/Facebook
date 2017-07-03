using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System;
using System.Drawing;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Azure.Documents;

namespace Facebook.Functions
{
    public class UserProfile 
    {
        public string UserId { get; set; }

        public string Url { get; set; }

        // TODO: Json property no worky
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }

    public static class ResizeImage
    {
        // Replace the subscriptionKey string value with your valid subscription key.
        static string subscriptionKey = File.ReadAllText("visionapi.key");
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/generateThumbnail";


        [FunctionName(nameof(ResizeImage))]
        public static async Task RunAsync([BlobTrigger("profile-pictures/{name}")] CloudBlockBlob myBlob, string name, [DocumentDB(databaseName: "Facebook", collectionName: "UserProfile", CreateIfNotExists = true)] DocumentClient client, [Blob("profile-pictures/{name}-168", FileAccess.ReadWrite)] CloudBlockBlob resizedBlob, TraceWriter log)
        {
            myBlob.Metadata.TryGetValue("UserId", out string userId);

            if (userId == null)
            {
                log.Info("Not processing blob. This is a resized image.");
                return;
            }

            resizedBlob.Properties.ContentType = myBlob.Properties.ContentType;
            using (var stream = new MemoryStream())
            {
                await myBlob.DownloadToStreamAsync(stream);
                stream.Position = 0;

                log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {stream.Length} Bytes");
                SendThumbnailRequest(stream, log, resizedBlob);

                
                var profilePictureUrl = client.CreateDocumentQuery<UserProfile>(
                    UriFactory.CreateDocumentCollectionUri("Facebook", "UserProfile"),
                    new FeedOptions() { MaxItemCount = -1 }).Where(x => x.UserId == userId).AsEnumerable().FirstOrDefault();
                if (profilePictureUrl != null)
                {
                    profilePictureUrl.Url = resizedBlob.Uri.AbsoluteUri;
                    await client.ReplaceDocumentAsync(
                    UriFactory.CreateDocumentUri("Facebook", "UserProfile", profilePictureUrl.id), profilePictureUrl);
                }
                else
                {
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("Facebook", "UserProfile"), new UserProfile() { Url = resizedBlob.Uri.AbsoluteUri, UserId = userId });
                }
            }
        }

        static async void SendThumbnailRequest(Stream image, TraceWriter log, CloudBlockBlob blob)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters.
            string requestParameters = "width=168&height=168&smartCropping=true";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                // Request body. Posts a locally stored JPEG image.
                byte[] byteData = memoryStream.ToArray();

                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses content type "application/octet-stream".
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // Execute the REST API call.
                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Display the response data.
                        log.Info("\nResponse:\n");
                        log.Info(response.ToString());

                        // Get the image data.
                        byte[] thumbnailImageData = await response.Content.ReadAsByteArrayAsync();
                        await blob.UploadFromByteArrayAsync(thumbnailImageData, 0, thumbnailImageData.Length);
                        //using (var ms = new MemoryStream(thumbnailImageData))
                        //{
                        //    var blah = Image.FromStream(ms);
                        //    blah.Save("test.png");
                        //}
                    }
                    else
                    {
                        // Display the JSON error data.
                        log.Info("\nError:\n");
                        log.Info(JsonPrettyPrint(await response.Content.ReadAsStringAsync()));
                    }
                }
            }
        }

        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>The formatted JSON string.</returns>
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}