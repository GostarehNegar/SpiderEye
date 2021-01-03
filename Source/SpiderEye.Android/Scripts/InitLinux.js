window._spidereye = new SpiderEyeBridge(function (e) {
	if (typeof XamarinBridge != 'object') {
		console.warn("XrmarinBridge is undefined");
	}
	XamarinBridge.PostMessage(e);
});
