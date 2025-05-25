import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import  {MatChipListbox, MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatRadioModule } from '@angular/material/radio';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HomeComponent } from '../home/home.component';
import { NavMenuComponent } from '../../controls//nav-menu/nav-menu.component';
import { DialogWindowComponent } from '../../controls//dialog-window/dialog-window.component';
import { BaseDetailComponent } from '../../controls//base-detail/base-detail.component';
import { ContentSourcePageComponent } from '..//content/content-source-page/content-source-page.component';
import { ViewPortComponent } from '../../controls//view-port/view-port.component';
import { WorkspaceComponent } from './workspace.component';
import { LazyImgDirective } from '../../../utils/lazy-load.directive';
import { ContentNotificationSectionComponent } from '../content/content-notifications-section/content-notifications-section.component';
import { ContentInfoPageComponent } from '../content/content-info-page/content-info-page.component';
import { ContentInfoSectionComponent } from '../content/content-info-section/content-info-section.component';
import { ContentItemsDetailComponent } from '@app/ui/controls/content/content-items-detail/content-items-detail.component';
import { ContentSourceSectionComponent } from '../content/content-source-section/content-source-section.component';
import { TradingSectionComponent } from '../trading/trading-section/trading-section.component';
import { OrderPageComponent } from '../trading/order-page/order-page.component';
import { MarkdownPipe } from '@app/utils/pipes/markdown.pipe';


@NgModule({ 
    declarations: [
        HomeComponent,
        NavMenuComponent,
        ContentNotificationSectionComponent,
        ContentInfoPageComponent,
        ContentInfoSectionComponent,
        DialogWindowComponent,
        BaseDetailComponent,
        ContentItemsDetailComponent,
        ContentSourceSectionComponent,
        ContentSourcePageComponent,
        ViewPortComponent,
        WorkspaceComponent,
        LazyImgDirective,
        TradingSectionComponent,
        OrderPageComponent,
        MarkdownPipe,
    ],
    exports: [MarkdownPipe, OrderPageComponent],
    imports: [
        BrowserModule,
        RouterModule,
        BrowserAnimationsModule,
        MatGridListModule,
        MatListModule,
        MatChipsModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatDialogModule,
        MatRadioModule,
        NgbModule,
        RouterModule,
        FormsModule,
        MatSliderModule,
        MatChipListbox
    ], 
    providers: [provideHttpClient(withInterceptorsFromDi())] })
export class WorkspaceModule {}
