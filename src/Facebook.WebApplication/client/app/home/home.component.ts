import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'home-component',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

    message: string = 'Hello World!';

    constructor() { }

    ngOnInit() {
    }
}