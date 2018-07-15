import { Component, OnInit } from '@angular/core';
import { MouseEvent } from '@agm/core';
import { User } from '../../models/user';
import { UserService } from '../../services/user/user.service';
import { LandmarkService } from '../../services/landmark/landmark.service';
import {LandMark} from '../../models/landMark'
import {marker} from '../../models/marker'
import { NgForm } from '@angular/forms'

@Component({
   moduleId: module.id,
   templateUrl: 'home.component.html',
   styleUrls: [ './home.component.css' ],
  providers:[LandmarkService]
})

export class HomeComponent implements OnInit{
  markers: marker[] = [];
  landMarker : LandMark[] = [];
  userProfile : User;
  landmarkService: LandmarkService;
  userservice : UserService;
  zoom: number = 8;
  lat: number;
  lng: number;
  name : string;
  searchText: string;
  userId : number;

  ngOnInit() {
    
    //get Current User Profile
    this.userservice.getProfile()
    .subscribe(
        data => {
            this.userProfile = data;
            this.name = data.firstName + ' ' + data.lastName;
            this.userId = data.id


            //Get All Land Marks and Notes --  Own + All Other Users
    this.landmarkService.getAll()
    .subscribe(
        data => {
          data.forEach(element => {
            let isAc : boolean;
            if(element.userId == this.userId){isAc = true;}
            else{isAc = false;}
            let tempMarker : marker = {
              lng : element.longitude,
              draggable : true,
              label : element.userName,
              lat : element.latitude,
              markerData : element,
              isActive : isAc
            }
            this.markers.push(tempMarker);
          });
        },
        error => {
            console.log('error')
    });
        },
        error => {
            console.log('error')
    });
  }

  constructor(private landmarkservice: LandmarkService, private userService : UserService){
    this.landmarkService = landmarkservice;
    this.userservice = userService;
    if (navigator)
    {
      navigator.geolocation.getCurrentPosition(
        pos => 
        {
          this.markers = this.markers;
          this.lng = +pos.coords.longitude;
          this.lat = +pos.coords.latitude;


          this.markers.push({
            lat: pos.coords.latitude,
            lng: pos.coords.longitude,
            label : "Curr",
            draggable: true,
            markerData: new LandMark(),
            isActive : true
          })
        });
    }
  }
  
  mapClicked($event: MouseEvent) {
    this.markers.push({
      lat: $event.coords.lat,
      lng: $event.coords.lng,
      draggable: true,
      markerData : new LandMark(),
      isActive : true,
      label : ""
    });
  }
  
  //Update Pointer Position on Drag
  markerDragEnd(m: marker, $event: MouseEvent) {

    //New coordinates after dragging.
    m.lat =  m.markerData.latitude = $event.coords.lat;;
    m.lng = m.markerData.longitude = $event.coords.lng;

    //Persist data after drag has occured
    this.landmarkService.update(m.markerData)
    .subscribe(
        data => {
            console.log(data);
        },
        error => {
            console.log(error);
        });
  }

  onSubmit(m : marker, form : NgForm, notesArea:any,lat:any,lng:any){
    let landMark : LandMark;


    //m.markerData is new for each new marker. So property lenth will be zero for
    //new markers. So creating new coordinate on this condition.
    if(m ==undefined || ( Object.keys(m.markerData).length === 0))
    {
      console.log('empty')
      landMark = m.markerData;
      landMark.latitude = m.lat;
      landMark.longitude = m.lng;
      landMark.text = m.markerData.text;
      landMark.userName = this.userProfile.username
      landMark.id = 0;
      landMark.userId = this.userProfile.id
      m.markerData.text = notesArea;
      this.landmarkService.create(m.markerData)
      .subscribe(
          data => {
              console.log(data);
          },
          error => {
              console.log(error);
          });

    }
    else{
      //m.markerData is not null. means this coordinate already exists in DB.
      //so updating this cooridnate.
      this.landmarkService.update(m.markerData)
      .subscribe(
          data => {
              console.log(data);
          },
          error => {
            console.log(error);
        });
      }
  }

  onSearch(searchText : string){
    if(searchText == undefined || searchText == '')
    {
      searchText = 'all'
    }
      this.landmarkService.search(searchText)
      .subscribe(
          data => {
            this.markers = [];
            data.forEach(element => {
              let tempMarker : marker = {
                 lng : element.longitude,
                 draggable : true,
                 label : element.userName,
                 lat : element.latitude,
                 markerData : element,
                 isActive : true
               }
               this.markers.push(tempMarker);
             });
          },
          error => {
              console.log(error);
          });
  }
  
  clickedMarker(lat : any, lng:any, markerInfo:any) {}
}
