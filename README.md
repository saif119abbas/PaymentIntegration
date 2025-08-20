# Payment Integration

A service to help you integrate **online payments** through the [OPEN BANK PROJECT](https://apiexplorer-ii-sandbox.openbankproject.com/operationid/OBPv3.0.0-getBranch?version=OBPv5.1.0) platform.

---

## **Features**
- Integrate direct transaction to bank account.

---

## **Getting Started**
### 1. Deploy the open api image in the localhost and manage the data:
  - Use this `docker-compose.yml`
    ```bash
      version: "3.9"
      services:
        redis:
          image: redis:latest
          container_name: obp-redis
          ports:
            - "6379:6379"
          networks:
            - obp-network
          restart: unless-stopped
      
        postgres:
          image: bitnami/postgresql:latest
          container_name: obp-postgres
          environment:
            - POSTGRES_DB=obp_api.db
            - POSTGRES_USER=obp_user
            - POSTGRES_PASSWORD=obp_password
          volumes:
            - ./postgres-data:/bitnami/postgresql
          ports:
            - "5432:5432"
          networks:
            - obp-network
          restart: unless-stopped
      
        obp-api:
          image: openbankproject/obp-api:latest
          container_name: obp-api
          env_file:
            - obp-api.env
          ports:
            - "8080:8080"
          depends_on:
            - redis
            - postgres
          networks:
            - obp-network
          restart: unless-stopped
      
      networks:
        obp-network:
          driver: bridge
    ```
-Use this environment file `obp-api.env`:
```bash
OBP_CONNECTOR=mapped
OBP_HOSTNAME=http://localhost:8080
OBP_DB_DRIVER=org.postgresql.Driver
OBP_DB_URL=jdbc:postgresql://postgres:5432/obp_api.db?user=obp_user&password=obp_password
OBP_MIGRATION_SCRIPTS_EXECUTE_ALL=true
OBP_MIGRATION_SCRIPTS_ENABLED=true
OBP_CONSUMERS_ENABLED_BY_DEFAULT=true
OBP_AUTHUSER_SKIPEMAILVALIDATION=true
OBP_DEV_PORT=8080
OBP_APIPATHZERO=obp
```
- Add the files inside a folder `Open Bank Project`, open PowerShell in the same directory as the  `Open Bank Project` folder, then run this command `docker-compose --env-file obp-api.env up -d`
- On your browser, open this link `http://localhost:8080` and create an account on a fake email, username, and generate an API key, and keep the information in a `.txt` file.
- User Postman for direct login on this URL `http://localhost:8080/my/logins/direct`, on the authorization header, add this DirectLogin username={{username}},password={{password}},consumer_key={{consumer_key}}. (The username that you registered on the `http://localhost:8080` and the consumer key from the Generate API KEY).
- Call this endpoint `http://localhost:8080/obp/v3.0.0/users/current` and add this in the authorization header `DirectLogin token={{token}}` the token from login endpoint, then take the user_id and save it.
- Run this command `docker-compose down` and edit the environment variable to keep the user ID on super admin IDs(OBP_SUPER_ADMIN_USER_IDS):
  ```bash
  OBP_CONNECTOR=mapped
  OBP_HOSTNAME=http://localhost:8080
  OBP_DB_DRIVER=org.postgresql.Driver
  OBP_DB_URL=jdbc:postgresql://postgres:5432/obp_api.db?user=obp_user&password=obp_password
  OBP_MIGRATION_SCRIPTS_EXECUTE_ALL=true
  OBP_MIGRATION_SCRIPTS_ENABLED=true
  OBP_CONSUMERS_ENABLED_BY_DEFAULT=true
  OBP_AUTHUSER_SKIPEMAILVALIDATION=true
  OBP_DEV_PORT=8080
  OBP_APIPATHZERO=obp
  OBP_SUPER_ADMIN_USER_IDS=11098f32-1c73-49c0-81c8-72508853a723
  OBP_ALLOW_TRANSACTION_REQUESTS=true
  OBP_TRANSACTIONREQUESTS_ENABLED=true
  OBP_TRANSACTIONREQUESTS_SUPPORTED_TYPES=SANDBOX_TAN,FREE_FORM,SEPA,CARD
```
- Run this command `docker-compose --env-file obp-api.env up -d`.
-Call create bank endpoint `{{baseURL}}/obp/v5.1.0/banks` this a from of body data
```json
{
  "id": "test-bank",
  "bank_code": "CGHZ",
  "full_name": "Test Bank",
  "logo": "logo url",
  "website": "www.openbankproject.com",
  "bank_routings": [
    {
      "scheme": "OBP",
      "address": "gh.29.uk"
    }
  ]
}
```
## **If u get an error for role, use this endpoint `{{baseURL}}/obp/v3.0.0/users/{{user-id}}/entitlements` add the bank_id and the role you want, the user_id for the id for super admin **
-Add Account from this endpoint `{{baseURL}}/obp/v5.1.0/banks/BANK_ID/accounts`, the bank ID of the bank you created, and the user ID in the body, using the ID of the super admin.
-Add Counterparty from this endpoint `{{baseURL}}/obp/v5.1.0/management/banks/BANK_ID/accounts/ACCOUNT_ID/owner/counterparties`, the bank ID of the bank you created, and the account ID of the account you created.
```json
{
  "name": "Saif Abbas",
  "description": "My landlord",
  "currency": "USD",
  "other_account_routing_scheme": "IBAN",
  "other_account_routing_address": "89b989e0-ece6-4e34-817e-f12e97e7d7f5",
  "other_account_secondary_routing_scheme": "IBAN",
  "other_account_secondary_routing_address": "89b989e0-ece6-4e34-817e-f12e97e7d7f5",
  "other_bank_routing_scheme": "OBP",
  "other_bank_routing_address": "test-bank",
  "other_branch_routing_scheme": "OBP",
  "other_branch_routing_address": "test-bank",
  "is_beneficiary": true,
  "bespoke": [
    {
      "key": "englishName",
      "value": "english Name"
    }
  ]
}```
For `other_account_routing_address`, `other_account_secondary_routing_address`, use the account ID that you have already created. For `other_bank_routing_address`, `other_branch_routing_address`, use the bank ID that you have already created.
-Create a new user from this endpoint `{{baseURL}}/obp/v5.1.0/users`
-Create an account from this point `{{baseURL}}/obp/v5.1.0/banks/BANK_ID/accounts` but use the user from the user you created from the previous endpoint.
-Create a Customer from this point `{{baseURL}}/obp/v5.1.0/banks/BANK_ID/customers` but use the user from the user you created from the previous endpoint.
-Create a new card from this endpoint `{{baseURL}}/obp/v5.1.0/management/banks/BANK_ID/cards`  in this form, use the customer ID and the account ID that you have already created
```json
{
  "card_number": "364435172576216",
  "card_type": "Debit",
  "name_on_card": "Customer Test",
  "issue_number": "2",
  "serial_number": "1324237",
  "valid_from_date": "2025-08-16T00:00:00Z",
  "expires_date": "2029-11-01T00:00:00Z",
  "enabled": true,
  "technology": "technology1",
  "networks": [
    ""
  ],
  "allows": [
    "credit",
    "debit"
  ],
  "account_id": "de6fc7a7-dc3c-4b19-ad04-d0dac219ee6e",
  "replacement": {
    "requested_date": "2025-08-20T00:00:00Z",
    "reason_requested": "RENEW"
  },
  "pin_reset": [
    {
      "requested_date": "2025-08-20T00:00:00Z",
      "reason_requested": "FORGOT"
    },
    {
      "requested_date": "2025-08-16T07:44:40Z",
      "reason_requested": "GOOD_SECURITY_PRACTICE"
    }
  ],
  "collected": "2025-08-20T00:00:00Z",
  "posted": "2025-08-20T00:00:00Z",
  "customer_id": "76ed6e6f-aa5d-4ec1-bbf0-5799647e8569", 
  "brand": "Visa"
}
```
-Add a balance to the last account you created from this endpoint `/obp/v5.1.0/banks/BANK_ID/accounts/ACCOUNT_ID/balances`
-Make sure to save each customer ID, user ID, and account ID to use them for another endpoint. And search this link `https://apiexplorer-ii-sandbox.openbankproject.com/operationid/OBPv3.0.0-getBranch?version=OBPv5.1.0` for get APIs you will need to get information. 
-Each api needs authorization,` DirectLogin token=TOKEN`. You get the TOKEN from the direct login endpoint.






