import { Routes } from '@angular/router';

import { ContentNotificationSectionComponent } from '../content/content-notifications-section/content-notifications-section.component';
import { ContentInfoSectionComponent } from '../content/content-info-section/content-info-section.component';
import { ContentSourceSectionComponent } from '../content/content-source-section/content-source-section.component';
import { ContentInfoPageComponent } from '../content/content-info-page/content-info-page.component';
import { ContentSourcePageComponent } from '../content/content-source-page/content-source-page.component';
import { TradingSectionComponent } from '../trading/trading-section/trading-section.component';
import { OrderPageComponent } from '../trading/order-page/order-page.component';


export const workspaceRoutes: Routes = [
	{ path: 'content', component: ContentNotificationSectionComponent },
	{ path: 'content/content-info', component: ContentInfoSectionComponent },
	{ path: 'content/sources', component: ContentSourceSectionComponent },
	{ path: 'content/content-info/:id', component: ContentInfoPageComponent },
	{ path: 'content/sources/:id', component: ContentSourcePageComponent },
	{ path: 'trading', component: TradingSectionComponent },
	{ path: 'trading/order/:id', component: OrderPageComponent },
];
