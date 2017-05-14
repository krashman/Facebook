import { SocialInteractions } from './social-interactions.model';
export class Post {
    id?: string;
    content: string;
    parentId?: string;
    socialInteractions?: SocialInteractions;
}