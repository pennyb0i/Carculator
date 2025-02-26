# The CARCULATOR 🚙🚗🚓
A simple project written in WPF that takes XML files containing car sales and creates price summaries grouped by model type

### XML file example 
This is the format that the app will accept:
```xml
<CarSales>
  <CarSale>
    <ModelName>Green car</ModelName>
    <SoldAtUtc>205-12-02T00:00:00Z</SoldAtUtc>
    <Price>300000</Price>
    <VAT>19</VAT>
  </CarSale>
  <CarSale>
    <ModelName>Red car</ModelName>
    <SoldAtUtc>2000-12-03T00:00:00Z</SoldAtUtc>
    <Price>210000</Price>
    <VAT>20</VAT>
  </CarSale>
  <CarSale>
    <ModelName>Police car</ModelName>
    <SoldAtUtc>2015-12-03T00:00:00Z</SoldAtUtc>
    <Price>900000</Price>
    <VAT>25</VAT>
  </CarSale>
</CarSales>
```
