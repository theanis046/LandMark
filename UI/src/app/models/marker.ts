import {LandMark} from './landMark'
export class marker {
	lat: number;
	lng: number;
	label?: string;
    draggable: boolean;
    markerData : LandMark;
    isActive:boolean;
}