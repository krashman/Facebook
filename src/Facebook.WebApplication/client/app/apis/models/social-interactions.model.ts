import { Injectable } from '@angular/core';
@Injectable()
export class SocialInteractions {
    id: string;
    postId: string;
    totalComments: number = 0;
    totalLikes: number = 0;
}