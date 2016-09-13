using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.FirebaseCloudMessaging.Models;

namespace Appiume.Apm.FirebaseCloudMessaging
{
    /// <summary>
    ///
    /// </summary>
    public class FirebaseCloudMessagingNotification : IFirebaseCloudMessagingNotification
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private string FormatData(Dictionary<string, string> data)
        {
            var builder = new StringBuilder();
            foreach (var item in data)
            {
                builder.AppendFormat("data.{0}={1}&", item.Key, item.Value);
            }

            return builder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="firebaseCloudMessagingRequest"></param>
        /// <returns></returns>
        public FirebaseCloudMessagingResponse Push(FirebaseCloudMessagingRequest firebaseCloudMessagingRequest)
        {
            var response = new FirebaseCloudMessagingResponse();

            try
            {
                response.Success = 0;
                response.Failure = 0;
                response.Results = null;

                var tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", firebaseCloudMessagingRequest.AuthorizationKey));

                var data = FormatData(firebaseCloudMessagingRequest.Data);
                var postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&priority=high&content-available=0&notification.body=Brian&notification.title=Hello Nha&notification.sound=default&" + data + "delay_while_idle=1&to=" + firebaseCloudMessagingRequest.To;

                var byteArray = Encoding.UTF8.GetBytes(postData);

                tRequest.ContentLength = byteArray.Length;

                using (var dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                var sResponseFromServer = tReader.ReadToEnd();

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                response.Success= 0;
                response.Failure = 1;
            }

            return response;
        }
    }
}
