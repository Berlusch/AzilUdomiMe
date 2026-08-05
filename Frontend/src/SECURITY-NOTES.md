## npm audit - known exception

**GHSA-qwww-vcr4-c8h2** (react-router, high)
npm audit reports a broad range (7.12.0 - 8.2.0) that includes the 
currently installed version 7.18.2, but according to the official 
GitHub security advisory, 7.18.2 is already the fixed version (the 
fix was introduced in 7.18.2 itself). Additionally, the vulnerability 
only affects the unstable RSC APIs, which this project does not use 
(plain SPA application).

IMPORTANT: Do NOT run "npm audit fix --force" for this item — it would 
downgrade react-router-dom to version 7.11.0, which is vulnerable to 
several other, more serious issues (XSS, RCE, open redirect).

Current version: react-router-dom@7.18.2 (intentionally kept)

Last reviewed: 2026-08-05