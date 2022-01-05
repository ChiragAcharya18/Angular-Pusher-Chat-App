import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import Pusher from 'pusher-js';
import { AfterViewChecked, ElementRef, ViewChild, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ChatUIExample';
  
  username: any = '';
  userId: any;
  messages: any[] = [];
  message = '';
  today: any; 
  timeStamp: any;
  userButtonFlag = "Add";

  @ViewChild('myDiv', { static: false })
  myScrollContainer!: ElementRef;

  //@ViewChild('myDiv') myDiv: any;

  constructor(private http: HttpClient, private toastr: ToastrService) {  
    this.getAllMessages().subscribe((data: any) => {
      if(data != null) {
        this.messages = data;
        console.log("Get all msgs: " + JSON.stringify(this.messages, null,4) );
      }
  });
  }

  ngOnInit(): void {
    // if(this.messages != null || this.messages != []){
    //   this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight + 10000;
    // }
    this.username = localStorage.getItem("perviouslyEnteredPusherChatUsername");
    Pusher.logToConsole = true;
    var pusher = new Pusher('86e7f851df82ccf3895c', {
      cluster: 'ap2'
    });
    var channel = pusher.subscribe('chat');
    channel.bind('message', (data: any) => {
      this.messages.push(data);
      //console.log("msg: "+data.datetime);
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight + 10000;
      //this.myScrollContainer.nativeElement.scrollHeight;
      });
  }

  changeUsername() {
    this.username = prompt("Enter a username: (Compulsory)");
    if(this.username == '') {
      this.changeUsername();
    } 
    localStorage.setItem("perviouslyEnteredPusherChatUsername", this.username);
    this.toastr.success("Username Changed", "Username");
  }

  submit(): void {
    if(this.username == '' || this.username == null) {
      this.changeUsername();
    }
    if(this.message == '') {
      return  
    }
    var msgTemp = {
      username: this.username,
      message: this.message
    }
    this.http.post('http://localhost:5000/api/messages', msgTemp).subscribe( (res: any) => {
      //this.messages = this.messages.reverse();
      this.message = '';
    });
  }

  changefn() {
    console.log("Change fn: " +this.message);
  }

  getDateTime() {
    this.today = new Date();
    var dd = String(this.today.getDate()).padStart(2, '0');
    var mm = String(this.today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = this.today.getFullYear();
    this.today = mm + '/' + dd + '/' + yyyy;
    this.timeStamp = "";//this.today.getHours() + ":" + this.today.getMinutes() + ":" + this.today.getSeconds();
    //this.timeStamp = String(this.today.getHours()) + ":" + String(this.today.getMinutes())
    //this.today.getSeconds()
  }

  getAllMessages():Observable<any[]> {
    //this.spinner.requestEnded(); 
    return this.http.get<any>('http://localhost:5000/api/getmessages');
  }

  name!: string;
  timeoutHandler!: any;
  public mouseup() {
    if (this.timeoutHandler) {
      clearTimeout(this.timeoutHandler);
      this.name = "canceled";
      this.timeoutHandler = null;
    }
  }

  public mousedown() {
    this.timeoutHandler = setTimeout(() => {
      this.name = "has been long pressed";
      this.timeoutHandler = null;
    }, 500);
  }

  

}
