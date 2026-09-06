# Souqy Backend API

This service exposes a simple product and category API under /api/v1/.

Key conventions
- API versioning: all endpoints are under /api/v1/
- Pagination: use query parameters `page` (1-based) and `pageSize`. Example: `/api/v1/products?page=1&pageSize=20`.
- Filtering: products can be filtered by category using `categoryId` query parameter: `/api/v1/products?categoryId={guid}`.
- Error contract: errors are returned as JSON with shape `{ "statusCode": <int>, "message": "..." }`.
- Caching: product listing responses are cached in-memory for 60 seconds keyed by page, pageSize and categoryId.

Validation
- Create/Update DTOs use data annotations. The API uses [ApiController] so invalid models return 400 with validation details.

Notes
- Database migrations are present under Infrastructure/Migrations but are not applied automatically. Run `dotnet ef database update --project Infrastructure --startup-project Souqy-Backend` with a valid connection string to apply migrations.
