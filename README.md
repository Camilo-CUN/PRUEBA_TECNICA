# PRUEBA TECNICA BACKEND 

## Instrucciones para Desplegar o ejecucion local

### Cambiar Credenciales de acceso a bd en appSetings Json 

### Ejecutar migraciones :
```
dotnet ef database update --context  ProductContext : Este comando Crea las tablas de productos en la bd seleccionada.
```
```
dotnet ef database update --context UserContext : Este comando Crea las tablas de usuarios en la bd seleccionada 
```

# En caso de querer usar la misma base de datos con registros podran usar el archivo .BAK adjunto en el correo de la prueba 

## Pasos para restaurar la bd a partir del backup
- `Abrir SSMS.`
- `Hacer clic derecho en Databases > Restore Database.`
- `Hacer clic derecho en Databases > Restore Database Seleccionar el archivo .bak y restaurar.`
