# 常用 EF Core Tool 指令
## 安裝工具
```bash
dotnet tool install --global dotnet-ef
```

## 修改連線字串
appsettings.json
``` json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
```

## 指令
### 建立 Migration
``` bash
dotnet ef migrations add InitialCreate -p CodeFirstSample -v
```

### 移除 Migration
``` bash
dotnet ef migrations remove -p CodeFirstSample -v
```

### 產生手動升級 SQL
``` bash
dotnet ef migrations script -p CodeFirstSample -v
```

### 手動升級資料庫
``` bash
dotnet ef database update -p CodeFirstSample -v
```

### 刪除資料庫
``` bash
dotnet ef database drop -p CodeFirstSample -v
```
