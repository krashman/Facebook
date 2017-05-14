import { Injectable } from '@angular/core';
@Injectable()
export class SocialInteractions {
    id: string;
    postId: string;
    totalComments: number;
    totalLikes: number;
}