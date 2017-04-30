import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { CoreModule } from '../core/core.module';
import { HomeRoutingModule } from './home-routing.module';
import { NewPostModule } from '../new-post/new.post.module';
import { PostsModule } from '../posts/posts.module';
@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        HomeRoutingModule,
        CoreModule,
        NewPostModule,
        PostsModule
    ],

    declarations: [
        HomeComponent
    ],

    exports: [
        HomeComponent
    ]
})

export class HomeModule { }