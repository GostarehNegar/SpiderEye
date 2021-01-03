using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpiderEye.Bridge;
using Xamarin.Forms.Platform.Android;

namespace SpiderEye.Android
{
	class AndroidWindow : IWindow
	{
		private Activity _activity;
		private AndroidWebView webView;
		public AndroidWindow(Activity activity, WebviewBridge bridge)
		{
			this._activity = activity;
			var layout = new LinearLayout(this._activity);
			layout.Orientation = Orientation.Vertical;
			this.webView = new AndroidWebView(this._activity,bridge);
			layout.AddView(this.webView);
			this._activity.SetContentView(layout);
		}

		public string Title { get; set; }
		public Size Size { get; set; }
		public Size MinSize { get; set; }
		public Size MaxSize { get; set; }
		public string BackgroundColor { get; set; }
		public bool CanResize { get; set; }
		public bool UseBrowserTitle { get; set; }
		public AppIcon Icon { get; set; }
		public bool EnableScriptInterface { get; set; }
		public bool EnableDevTools { get; set; }

		public IWebview Webview => this.webView;

		public event EventHandler Shown;
		public event CancelableEventHandler Closing;
		public event EventHandler Closed;

		public void Close()
		{
			//throw new NotImplementedException();
		}

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		public void SetWindowState(WindowState state)
		{
			//throw new NotImplementedException();
		}

		public void Show()
		{
			//throw new NotImplementedException();
		}
	}
}