export const environment = {
	production: false,
	contentHost: 'https://localhost:64540',
	authHost: 'https://localhost:44393',
	notificationsHost: "https://localhost:44399",
	binanceHost: 'https://localhost:7203',
	auth: {
		flowName: "B2C_1_maid-dev",
		authorityDomain: "maidadur.b2clogin.com",
		authority: "https://maidadur.b2clogin.com/maidadur.onmicrosoft.com/B2C_1_maid-dev",
		clientId: '737a7083-5747-4b3f-8ffc-dff956a240d2',
		ui_scopes: ["https://maidadur.onmicrosoft.com/737a7083-5747-4b3f-8ffc-dff956a240d2"],
		app_scopes: [
			"https://maidadur.onmicrosoft.com/f1f15b09-3fdc-4a1b-9b59-2089fb42add3/tasks.write", 
			"https://maidadur.onmicrosoft.com/f1f15b09-3fdc-4a1b-9b59-2089fb42add3/tasks.read"
		],
		redirectUri: 'https://localhost:4200/'
	}
  };
  