PRUEBA TECNICA BACKEND 

Instrucciones para Desplegar o ejecucion local

-- Cambiar Credenciales de acceso a bd en appSetings Json 

-- Ejecutar migraciones :

  dotnet ef database update --context  ProductContext : Este comando Crea las tablas de productos en la bd seleccionada.
  dotnet ef database update --context UserContext : Este comando Crea las tablas de usuarios en la bd seleccionada 
