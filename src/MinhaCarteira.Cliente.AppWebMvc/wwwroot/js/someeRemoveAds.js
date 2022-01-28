$(document).ready(function () {
	SomeeAdsRemover();

	setTimeout(function () {
		SomeeAdsRemover();
	}, 100);
});

function SomeeAdsRemover() {
	$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
	$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
	$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
	$("div[onmouseover='S_ssac();']").remove();
	$("center").remove();
	$("div[style='height: 65px;']").remove();

	/*
	$("div[style='width: 100%; color: White; font-family: &quot;Helvetica Neue&quot;, &quot;Lucida Grande&quot;, &quot;Segoe UI&quot;, Arial, Helvetica, Verdana, sans-serif; font-size: 11pt;]").remove();
	$("div[style='margin-right: auto; margin-left: auto; display: table;  padding:9px; font-size:13pt;]").remove();
	$("div[style=' text-decoration: none; color:White; margin-right:6px; margin-left:6px;]").remove();
	$("div[style='margin-right: auto; margin-left: auto; display: table; font-size: 9pt;]").remove();
	*/
}