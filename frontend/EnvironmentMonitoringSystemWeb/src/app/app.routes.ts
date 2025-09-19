import { Routes } from '@angular/router';
import { Devices } from './pages/devices/devices';
import { Home } from './pages/home/home';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: Home },
    { path: 'devices', component: Devices },
];
