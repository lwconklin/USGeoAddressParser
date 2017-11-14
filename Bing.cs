// <copyright file="Bing.cs" company="Air Osprey">
//     MIT License (MIT). All rights reserved
// </copyright>
// <author>Larry Conklin</author>
// <summary>US Address Parsing class.</summary>

namespace USGeoAddressParser
{
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

    internal class Bing 
    {
        private string userBingMapKey = "";
 
        public HttpWebRequest WebGet() 
        {
            string url = @"http://dev.virtualearth.net/REST/v1/Locations/US/OK/74012/tulsa/4705 south 129th east avenue?o=json&key=" + this.userBingMapKey;
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            return request;
        }

        public void WebReponse() 
        {
            Stream resStream = null;
            const int MaxReadCharacters = 5125; // 5k
            StringBuilder sb = new StringBuilder();
            WebResponse response = this.WebGet().GetResponse();
            resStream = this.WebGet().GetResponse().GetResponseStream();

            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            char[] read = new char[MaxReadCharacters]; 
            int count = reader.Read(read, 0, MaxReadCharacters);
            while (count > 0) 
            {
                sb.Append(read, 0, count);
                count = reader.Read(read, 0, MaxReadCharacters);
            }

            MemoryStream mStream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(sb.ToString()));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(BingDataContractResponse));
            object objResponse = jsonSerializer.ReadObject(mStream);
            BingDataContractResponse jsonResponse = objResponse as BingDataContractResponse;
  
            // Display the status.
            //// Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            Stream dataStream = resStream;

            // Open the stream using a StreamReader for easy access.
            StreamReader reader2 = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader2.ReadToEnd();

            // Display the content.
            //// Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.
            Location location = (Location)jsonResponse.ResourceSets[0].Resources[0];

            //// if (location.Confidence == "High")
            //// {
            ////    Console.WriteLine(location.Address.FormattedAddress);
            //// }

        Console.WriteLine();

            reader.Close();
            response.Close();
            resStream.Close();
        }
    } // end of class
} // end of namespace
