import { FacebookPage } from './app.po';

describe('facebook App', () => {
  let page: FacebookPage;

  beforeEach(() => {
    page = new FacebookPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
