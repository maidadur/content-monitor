export const environment = {
	production: true,
	contentHost: 'https://content-api.{domain}',
	authHost: '',
	notificationsHost: "https://push.{domain}",
	auth: {
		flowName: "B2C_1_maid-prod",
		authorityDomain: "maidadur.b2clogin.com",
		authority: "https://maidadur.b2clogin.com/maidadur.onmicrosoft.com/B2C_1_maid-prod",
		clientId: 'd213892c-7e41-45ab-be19-4d0028fa5468',
		ui_scopes: ["https://maidadur.onmicrosoft.com/d213892c-7e41-45ab-be19-4d0028fa5468"],
		content_monitor_scopes: [
			"https://maidadur.onmicrosoft.com/feb7657b-a552-4ba3-bdb5-5d8b0c85d4db/tasks.write", 
			"https://maidadur.onmicrosoft.com/feb7657b-a552-4ba3-bdb5-5d8b0c85d4db/tasks.read"
		],
		redirectUri: 'https://dwork.me'
	}
  };
  