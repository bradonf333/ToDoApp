import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-todo-delete',
  templateUrl: './todo-delete.component.html',
  styleUrls: ['./todo-delete.component.scss']
})
export class TodoDeleteComponent implements OnInit {
  @Input() userId: string;
  @Input() title: string;

  constructor() {}

  ngOnInit() {}
}
