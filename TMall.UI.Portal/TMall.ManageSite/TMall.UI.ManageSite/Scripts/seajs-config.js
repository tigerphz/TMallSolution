/*map start*/
seajs.production = false;
if (seajs.production) {
    seajs.config({
        map: [
	[
		"management/addmenu/main.js",
		"management/addmenu/main-6a6e33e5d99a162ea70630805241d2d6.js"
	],
	[
		"management/updatemenu/main.js",
		"management/updatemenu/main-91f6921d25212aa20d2effd1c93aa1b4.js"
	]
]
    });
}
/*map end*/
seajs.config({
    preload: ['seajs/2.0.0/plugin-style.js',
     'seajs/2.0.0/plugin-nocache.js'
    ],
    alias: {
        'knockout': 'gallery/knockout/2.2.1/knockout',
        "knockoutValidation": "gallery/knockout/2.2.1/knockout-Validation",
        "knockoutExtentions": "gallery/knockout/2.2.1/knockout-Extentions",
        "easyui": "gallery/easyui/1.3.0/jquery.easyui.min",
    }
});