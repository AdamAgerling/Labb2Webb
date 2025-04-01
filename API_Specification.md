# 📘 API Specification – ynet Webstore

- **Version:** 1.0  
- **Base-URL:** `https://localhost:7278/api/`  
- **Format:** JSON  
- **Auth:** JWT (Bearer Token)

---

## 🔐 Authentication

### Login

> ```http
> POST /api/Auth/login
> ```

**Description:** Authenticates user and returns a JWT token.

### Request
```json
{
  "email": "admin@admin.com",
  "password": "1234"
}
```

### Response
```json
{
  "token": "jwt.token.here",
  "fullName": "Admin Adminsson"
}
```

| Code               | Description      |
|--------------------|------------------|
| `200 OK`           | Login successful |
| `401 Unauthorized` | Invalid login    |

---

## 🛍️ Products

### Get All Products

> ```http
> GET /api/Product
> ```

**Description:** Fetch all available products.  
**Auth:** Public

### Response (example)
```json
[
  {
    "id": 1,
    "productNumber": "GPU-1001",
    "productName": "NVIDIA RTX 5090",
    "productDescription": "World’s fastest GPU for gamers and creators.",
    "price": 2199.99,
    "productCategory": "Graphics Card",
    "status": 0
  }
]
```

---

### Get Product by ID

> ```http
> GET /api/Product/{id}
> ```

**Description:** Get details of a specific product.  
**Auth:** Public

---

### Search Products

> ```http
> GET /api/Product/search?name=rtx
> ```

**Description:** Search for products by name.  
**Auth:** Public

---

### Get Products by Category

> ```http
> GET /api/Product/category/{category}
> ```

**Description:** Get products filtered by category.  
**Auth:** Public

---

### Create New Product

> ```http
> POST /api/Product
> ```

**Description:** Create a new product.  
**Auth:** Admin only

### Request
```json
{
  "productNumber": "SSD-300",
  "productName": "Samsung 990 Pro",
  "productDescription": "Superfast M.2 NVMe SSD.",
  "price": 189.99,
  "productCategory": "Storage"
}
```

---

### Update Product

> ```http
> PUT /api/Product/{id}
> ```

**Description:** Update details of a product.  
**Auth:** Admin only

---

### Delete Product

> ```http
> DELETE /api/Product/{id}
> ```

**Description:** Deletes or marks product as discontinued.  
**Auth:** Admin only

---

## 👤 Customers

### Register New Customer

> ```http
> POST /api/Customer
> ```

**Description:** Register a new customer.  
**Auth:** Public

---

### Get All Customers

> ```http
> GET /api/Customer
> ```

**Description:** Fetch all registered customers.  
**Auth:** Admin only

---

### Get Customer by Email

> ```http
> GET /api/Customer/{email}
> ```

**Description:** Get details of one customer.  
**Auth:** Admin only

---

### Get Logged In Customer Profile

> ```http
> GET /api/Customer/profile
> ```

**Description:** Returns profile info for logged in user.  
**Auth:** Customer only

---

### Update Customer

> ```http
> PUT /api/Customer/{email}
> ```

**Description:** Update customer info.  
**Auth:** Customer or Admin

---

## 📦 Orders

### Get All Orders

> ```http
> GET /api/Order
> ```

**Description:** Returns all orders.  
**Auth:** Admin only

---

### Get Order by ID

> ```http
> GET /api/Order/{id}
> ```

**Description:** Get specific order by ID.  
**Auth:** Admin or Customer (own order)

---

### Get Orders by Customer Email

> ```http
> GET /api/Order/customer?email=example@example.com
> ```

**Description:** Get all orders from a specific customer.  
**Auth:** Admin or Customer

---

### Create New Order

> ```http
> POST /api/Order
> ```

**Description:** Place a new order.  
**Auth:** Customer only

---

### Update Order (e.g. status)

> ```http
> PUT /api/Order/{id}
> ```

**Description:** Update order, including status.  
**Auth:** Admin only

---

### Update Only Order Status

> ```http
> PUT /api/Order/{id}/status
> ```

**Description:** Change order status (Unhandled → Packing → Sent → Delivered).  
**Auth:** Admin only

### Request
```json
"status": "Sent"
```

---

### Delete Order

> ```http
> DELETE /api/Order/{id}
> ```

**Description:** Delete an order.  
**Auth:** Admin only

---

## 🔐 Auth Header Example

All endpoints requiring authorization must include a Bearer token in the header:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```
