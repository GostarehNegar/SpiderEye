using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using SpiderEye.Bridge;
using _Android = global::Android;

namespace SpiderEye.Android
{
	internal class AndroidWebView : _Android.Webkit.WebView, IWebview
	{
		public class JSValueCallBack : Java.Lang.Object, IValueCallback
		{
			private TaskCompletionSource<string> _source;
			public JSValueCallBack(TaskCompletionSource<string> source)
			{
				this._source = source;
			}

			public void OnReceiveValue(Java.Lang.Object value)
			{
				_source.SetResult(value?.ToString());
			}
		}
		public class JSInterFace : Java.Lang.Object
		{
			AndroidWebView _contex;
			WebviewBridge bridge;
			public JSInterFace(AndroidWebView context)
			{
				this._contex = context;

			}
			[Export]
			[JavascriptInterface]
			public void PostMessage(Java.Lang.String message)
			{
				this._contex.HandleScriptCall(message.ToString());

				//var task = bridge.HandleScriptCall(message.ToString());
			}
		}


		private WebviewBridge bridge;
		internal AndroidWebView(Context context, WebviewBridge bridge) : base(context)
		{
			this.Settings.JavaScriptEnabled = true;
			this.AddJavascriptInterface(new JSInterFace(this), "XamarinBridge");
			this.SetWebViewClient(new AndroidWebViewClient(this));
			this.bridge = bridge;


		}

		public void HandleScriptCall(string message)
		{
			var rask = this.bridge.HandleScriptCall(message);
		}

		public event PageLoadEventHandler PageLoaded;

		public Task<string> ExecuteScriptAsync(string script)
		{
			var source = new TaskCompletionSource<string>();
			this.EvaluateJavascript(script, new JSValueCallBack(source));
			return source.Task;
		}

		public void LoadUri(Uri uri)
		{
			this.LoadUrl(uri.ToString());
		}
	}
	internal class AndroidWebViewClient : _Android.Webkit.WebViewClient
	{
		public AndroidWebView WebView { get; set; }
		public AndroidWebViewClient(AndroidWebView view)
		{
			this.WebView = view;
		}
		public override void OnPageFinished(WebView view, string url)
		{
			string initScript = SpiderEye.Resources.GetInitScript("Linux");
			this.WebView.EvaluateJavascript(initScript, null);
			base.OnPageFinished(view, url);

		}
		public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
		{
			//return false;
			return base.ShouldOverrideUrlLoading(view, request);
		}
		public override WebResourceResponse ShouldInterceptRequest(WebView view, IWebResourceRequest request)
		{
			if (request != null && request.Url != null)
			{
				var stream = Application.ContentProvider
					.GetStreamAsync(new Uri(request.Url.ToString()))
					.ConfigureAwait(false).GetAwaiter().GetResult();
				if (stream != null)
				{
					var responseHeaders = new Dictionary<string, string>()
					{
						{ "Cache-Control", "no-cache" },
					};
					var url = request.Url.ToString().ToLowerInvariant();
					var mime = MimeTypeMap.Singleton.GetMimeTypeFromExtension(
						MimeTypeMap.GetFileExtensionFromUrl(
						request.Url.ToString()));
					Log.Info(Globals.LogTag,
						$"Resource '{url}' successfully found in ContentProvider. We will return it the WebResource");
					return new WebResourceResponse(mime, "UTF-8", 200, "OK", responseHeaders, stream);
				}
				else
				{

				}
			}

			return base.ShouldInterceptRequest(view, request);
		}
	}



}