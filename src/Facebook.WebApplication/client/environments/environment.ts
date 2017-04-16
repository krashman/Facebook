// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
let IDENTITY_PROVIDER_HOST = 'http://localhost:5001'
export const environment = {
  production: false,
  TOKEN_ENDPOINT: `${IDENTITY_PROVIDER_HOST}/connect/token`,
  REVOCATION_ENDPOINT: `${IDENTITY_PROVIDER_HOST}/connect/revocation`,
  USERINFO_ENDPOINT: `${IDENTITY_PROVIDER_HOST}/connect/userinfo`,

  CLIENT_ID: "Facebook",

  /**
   * Resource Owner Password Credential grant.
   */
  GRANT_TYPE: "password",

  /**
   * The Web API, refresh token (offline_access) & user info (openid profile roles).
   */
  SCOPE: "WebAPI offline_access openid profile roles"
};
