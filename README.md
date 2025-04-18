# DataVaultDotNet

**DataVaultDotNet** is a powerful, extensible .NET framework for building and executing **Data Vault 2.0 pipelines**. It supports **SQL Server**, **Snowflake**, **AutoMapper**, **custom mapping**, and offers both **code-first** and **YAML-based** pipeline definitions.

The framework enables you to define all layers of a Data Vault model—**staging, hubs, links, satellites, PITs, bridges, and business vault calculations**—with complete control and documentation support.

---

## 🔥 Features

- ✅ Fluent **C# DSL** for pipeline creation  
- ✅ **AutoMapper** and **custom mapping class** support  
- ✅ Database support for **SQL Server** and **Snowflake**  
- ✅ **Point-in-Time (PIT)** and **Bridge** table generation  
- ✅ Optional **YAML pipeline definitions**  
- ✅ Auto-generated **Markdown or HTML documentation**  
- ✅ Modular and testable architecture  
- ✅ Ready for use in **CI/CD pipelines**

---

## 📦 Quick Start

### 1. Install the Core Library (coming soon)

```bash
dotnet add package DataVaultDotNet
```

### 2. Configure Dependency Injection

```csharp
services.AddDataVaultPipeline(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
    // or:
    // options.UseSnowflake(Configuration["Snowflake:Connection"]);
});
```

### 3. Define and Execute Your Pipeline

```csharp
var pipeline = pipelineBuilder
    .WithSource("CustomerImport")
        .FromSql("SELECT * FROM legacy_customer")
        .ToStagingTable("stg_Customer")
        .MapTo<CustomerModel>()
    .WithHub<CustomerHub>().UseAutoMapper()
    .WithSatellite<CustomerDetailsSatellite>().UseMapper<CustomerDetailsSatelliteMapper>()
    .Build();

await pipeline.ExecuteAsync();
```

---

## ⚙️ Pipeline Execution

Run pipelines programmatically or from the CLI (CLI coming soon):

```csharp
await pipeline.ExecuteAsync(cancellationToken);
```

---

## 📄 Static Documentation

Generate docs with entity details, lineage, hash strategies, and load logic:

```csharp
await pipeline.GenerateDocumentation("docs/output", format: "markdown");
```

Future support for Mermaid.js, C4 diagrams, and full HTML static site is planned.

---

## 🧩 Usage Guide

### Staging

Extract from SQL, files, or APIs.

```csharp
.WithSource("CustomerImport")
    .FromSql("SELECT * FROM legacy_customer")
    .ToStagingTable("stg_Customer")
    .MapTo<CustomerModel>()
```

---

### Hubs

Capture unique business keys.

```csharp
.WithHub<CustomerHub>().UseAutoMapper()
// or
.WithHub<CustomerHub>().UseMapper<CustomerHubMapper>()
```

```csharp
public class CustomerHub
{
    [BusinessKey] public string CustomerId { get; set; }
}
```

---

### Satellites

Track descriptive and historical data.

```csharp
.WithSatellite<CustomerDetailsSatellite>().UseMapper<CustomerDetailsSatelliteMapper>()
```

```csharp
public class CustomerDetailsSatellite
{
    [ParentKey] public string CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

---

### Links

Capture relationships across hubs.

```csharp
.WithLink<CustomerProductLink>()
    .UsingKeys(src => src.CustomerId, src => src.ProductId)
```

```csharp
public class CustomerProductLink
{
    [BusinessKey] public string CustomerId { get; set; }
    [BusinessKey] public string ProductId { get; set; }
}
```

---

### Point-in-Time (PIT)

For fast point-in-time queries over satellites.

```csharp
.WithPIT("CustomerPIT")
    .Tracking<CustomerDetailsSatellite>()
    .At("LoadDate")
```

---

### Bridge Tables

Join multiple satellites and links for performance.

```csharp
.WithBridge("CustomerProductBridge")
    .ForLink<CustomerProductLink>()
    .IncludingSatellites<CustomerDetailsSatellite, ProductDetailsSatellite>()
```

---

### Business Vault Calculations

Use code to calculate derived attributes.

```csharp
.WithBusinessVault("CustomerClassification")
    .Calculate((ctx, row) => new CustomerClassification
    {
        CustomerId = row.CustomerId,
        Segment = row.TotalSpend > 10000 ? "Premium" : "Standard"
    })
```

---

### Information Marts
After the business vault layer, DataVaultDotNet allows you to define Information Marts in two styles:

Flat output tables: Denormalized tables for fast consumption

Star schemas: Fact + dimension models with surrogate key joins

Multi-fact star schemas: Share conformed dimensions across fact tables

#### Flat Output Table
```csharp
pipelineBuilder
    .WithFlatMart("CustomerProfileMart")
    .FromEntities<CustomerHub, CustomerDetailsSatellite, CustomerClassification>()
    .IncludeColumns(c => c.CustomerId, c => c.Name, c => c.Segment)
    .MaterializeTo("mart.CustomerProfile");
```

#### Star Schema
```csharp
pipelineBuilder
    .WithStarSchema("SalesMart")
    .AddDimension<CustomerDimension>()
    .AddDimension<ProductDimension>()
    .AddFact<SalesFact>(cfg => cfg
        .Map(f => f.CustomerId).ToDimension<CustomerDimension>()
        .Map(f => f.ProductId).ToDimension<ProductDimension>()
        .WithMeasures(f => f.TotalAmount, f => f.Quantity))
    .MaterializeTo("mart.Sales");
```

#### Multi-Fact Star Schema
```csharp
pipelineBuilder
    .WithStarSchema("RevenueMart")
    .AddDimension<CalendarDimension>()
    .AddFact<OnlineSalesFact>(cfg => cfg
        .Map(f => f.DateId).ToDimension<CalendarDimension>())
    .AddFact<StoreSalesFact>(cfg => cfg
        .Map(f => f.DateId).ToDimension<CalendarDimension>())
    .MaterializeAll();
```

--- 

## ✨ Entity Configuration Styles
### Option 1: Fluent Configuration (Recommended)
```csharp
modelBuilder
    .Entity<CustomerHub>(e =>
    {
        e.HasBusinessKey(c => c.CustomerId);
        e.HasAttribute(c => c.Region);
    });

modelBuilder
    .Entity<SalesFact>(e =>
    {
        e.HasMeasure(f => f.Quantity);
        e.HasMeasure(f => f.TotalAmount);
        e.HasForeignKey(f => f.CustomerId).To<CustomerDimension>();
        e.HasForeignKey(f => f.ProductId).To<ProductDimension>();
    });
```

This mirrors EF Core’s model builder style for developer familiarity.

---

### Option 2: Attribute-based Entity Tagging
#### Dimension Example
```csharp
[Dimension]
public class CustomerDimension
{
    [BusinessKey] public string CustomerId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
}
```
#### Fact Example
```csharp
[Fact]
public class SalesFact
{
    [ForeignKey(typeof(CustomerDimension))] public string CustomerId { get; set; }
    [ForeignKey(typeof(ProductDimension))] public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}
```

### 🧪 Usage: Star Schema Definition
```csharp
pipelineBuilder
    .WithStarSchema("OrderMart")
    .AddDimension<OrderDateDimension>()
    .AddDimension<CustomerDimension>()
    .AddFact<OrderFact>()
    .Build();
```
Or define entirely with attributes:

```csharp
[Fact]
public class OrderFact
{
    [ForeignKey(typeof(CustomerDimension))] public string CustomerId { get; set; }
    [ForeignKey(typeof(OrderDateDimension))] public string OrderDate { get; set; }
    public int UnitsOrdered { get; set; }
    public decimal Revenue { get; set; }
}
```

---

## 📝 YAML Pipeline Support

For low-code users or configuration as code:

```yaml
source:
  name: CustomerImport
  query: SELECT * FROM legacy_customer
  model: CustomerModel

hub:
  name: CustomerHub
  keys: [CustomerId]

satellite:
  name: CustomerDetailsSatellite
  columns: [Name, Email]
```

Use it in code:

```csharp
var pipeline = pipelineBuilder.FromYaml("pipelines/customer-pipeline.yaml").Build();
```

---

## 🧱 Folder Structure

```
DataVaultDotNet/
│
├── src/
│   ├── DataVaultDotNet.Core/
│   ├── DataVaultDotNet.SqlServer/
│   ├── DataVaultDotNet.Snowflake/
│   ├── DataVaultDotNet.Cli/
│   └── DataVaultDotNet.Examples/
│
├── tests/
│   └── DataVaultDotNet.Core.Tests/
│
├── docs/
├── pipeline-samples/
│   └── customer-pipeline.yaml
│
├── .editorconfig
├── Directory.Build.props
├── Contributing.md
├── README.md
└── DataVaultDotNet.sln
```

---

## 🚀 Roadmap

- [ ] Fluent C# API
- [ ] AutoMapper and custom mappers
- [ ] Staging, Hubs, Links, Satellites
- [ ] PIT and Bridge support
- [ ] Business Vault calculations
- [ ] YAML support
- [ ] Static visual diagrams
- [ ] CLI runner
- [ ] Deployment automation
- [ ] OpenTelemetry and audit hooks

---

## 🤝 Contributing

Contributions are welcome! Open issues, submit PRs, or fork and experiment.

See [CONTRIBUTING.md](CONTRIBUTING.md) for more details.

---

## 📄 License

[MIT License](LICENSE)
