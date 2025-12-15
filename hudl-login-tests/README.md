# Hudl Login Tests

Selenium-based login tests for Hudl using LightBDD and xUnit.

## Setup

1. Add your credentials to `testSettings.json`:
   ```json
   "Hudl": {
     "ValidUser": {
       "Email": "your-email@example.com",
       "Password": "your-password"
     }
   }
   ```

2. Run tests:
   ```
   dotnet test
   ```

## Headless Mode

Set environment variable `BROWSER_HEADLESS=true` or update `testSettings.json`.
