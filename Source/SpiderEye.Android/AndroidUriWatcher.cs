using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SpiderEye.Android
{
	class AndroidUriWatcher : IUriWatcher
	{
        private readonly Uri devServerUri;

        public AndroidUriWatcher(string devServerUri)
        {
            this.devServerUri = new Uri(devServerUri);
        }

        public Uri CheckUri(Uri uri)
        {
            // this is only called in debug mode
            //CheckDevUri(ref uri);

            return uri;
        }

        [Conditional("DEBUG")]
        private void CheckDevUri(ref Uri uri)
        {
            // this changes a relative URI (e.g. /index.html) to
            // an absolute URI with the Angular dev server as host
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(devServerUri, uri);
            }
        }
    }
}