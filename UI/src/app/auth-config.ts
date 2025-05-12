import { MsalModule } from '@azure/msal-angular';
import {  Configuration, BrowserCacheLocation, PublicClientApplication, InteractionType } from '@azure/msal-browser';

import { environment } from 'environments/environment';

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;
 
export const b2cPolicies = {
     names: {
         signIn: environment.auth.flowName,
     },
     authorities: {
        signIn: {
             authority: environment.auth.authority,
         },
     },
     authorityDomain: environment.auth.authorityDomain
 };
 
export const msalConfig: Configuration = {
     auth: {
         clientId: environment.auth.clientId,
         authority: b2cPolicies.authorities.signIn.authority,
         knownAuthorities: [b2cPolicies.authorityDomain],
         redirectUri: environment.auth.redirectUri,
     },
     cache: {
         cacheLocation: BrowserCacheLocation.LocalStorage,
         storeAuthStateInCookie: isIE,
     }
 }

 export const protectedResources = {
    content_monitor_api: {
      endpoint: environment.contentHost  + '/*',
      scopes: environment.auth.app_scopes,
    },
    binance_api: {
      endpoint: environment.binanceHost  + '/*',
      scopes: environment.auth.app_scopes,
    },
  }

 export const msalModule = MsalModule.forRoot(
    new PublicClientApplication(msalConfig),
    {
        interactionType: InteractionType.Redirect,
        authRequest: {
            scopes: environment.auth.ui_scopes,
        },
    },
    {
        interactionType: InteractionType.Redirect,
        protectedResourceMap: new Map([
            [protectedResources.content_monitor_api.endpoint, protectedResources.content_monitor_api.scopes],
            [protectedResources.binance_api.endpoint, protectedResources.binance_api.scopes],
        ]),
    }
);

