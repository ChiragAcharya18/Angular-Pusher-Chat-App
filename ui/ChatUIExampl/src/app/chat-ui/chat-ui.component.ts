import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat-ui',
  templateUrl: './chat-ui.component.html',
  styleUrls: ['./chat-ui.component.css']
})
export class ChatUiComponent implements OnInit {

  constructor() { }

  msgs = ["abc", "abc","abc","abc", "abc","abc","abc", "abc","abc","abc", "abc","abc"]

  ngOnInit(): void {
  }

}
