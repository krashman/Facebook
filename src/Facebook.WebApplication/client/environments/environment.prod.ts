
let IDENTITY_PROVIDER_HOST = 'http://localhost:5001'
export const environment = {
  production: true,

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
