# Environment Monitoring System 
## Sistema de Monitoramento de Ambiente

Sistema de monitoramento de dispositivos IoT com backend em .NET, frontend em Angular e persistência em SQLite. Permite cadastro de dispositivos, recebimento e visualização de eventos em tempo real.

**Justificativa para o uso da arquitetura:**
A aplicação foi desenvolvida utilizando uma arquitetura em camadas simplificada (Clean Architecture), composta por Domain, Infrastructure, Application e API, com camada adicional de Testes.
Essa escolha permite uma organização clara onde cada camada tem responsabilidades bem definidas, facilitando a compreensão e o trabalho. 
Alterações em uma camada, como a de persistencia por exemplo, não impactam outras, tornando a evolução do sistema um pouco mais segura.
Tem também a questão da robustez e testabilidade: a lógica de negócio é isolada e testável independentemente da infraestrutura.
A separação também permite aplicar melhorias localizadas, como cache ou processamento assíncrono, sem afetar outras partes do sistema.
Escalabilidade e extensibilidade: novas funcionalidades ou integrações podem ser adicionadas sem grandes impactos.
Outro detalhe é que eu puxei um pouco para a minha sardinha, pois foi a arquitetura que mais trabalhei o que facilitou a entrega. 

---

## Funcionalidades

### Gerenciamento de Dispositivos (CRUD)
- **POST /api/devices**: Cadastra um novo dispositivo.
  - Após salvar no banco, chama a API Mock de IoT (`POST /register`) e armazena o `integrationId`.
- **GET /api/devices**: Lista todos os dispositivos.
- **GET /api/devices/{id}**: Obtém detalhes de um dispositivo específico.
- **PUT /api/devices/{id}**: Atualiza informações do dispositivo no banco.
- **DELETE /api/devices/{id}**: Remove um dispositivo.
  - Também chama a API Mock de IoT (`DELETE /unregister/{integrationId}`).

### Recebimento de Eventos
- **POST /api/events**: Endpoint de callback para receber eventos do simulador.
  - Valida payload.
  - Persiste eventos no banco, associando ao dispositivo correspondente.
  - Disponibiliza os eventos para visualização em tempo real.

---

## Frontend
- Feito em **Angular** usando Typescript.
- **Dashboard de Eventos**: Tela inical mostrando eventos em tempo real.
  - Eventos com `isAlarm: true` são destacados para chamar atenção do operador.
- **Dispositivos**: Tela para criar, listar, editar e deletar dispositivos.

---

## Arquitetura

O sistema foi desenvolvido utilizando uma **arquitetura em camadas simplificada (Clean Architecture)**:

1. **Domain**: Entidades e interfaces do domínio.
2. **Infrastructure**: Implementação de persistência e integração com APIs externas.
3. **Application**: Lógica de negócio e coordenação entre API e persistência.
4. **API**: Controllers e endpoints HTTP.
5. **Testes**: Cobrem alguns cenários de sucesso e falha do backend. (xUnit)

---

## 🔹 Configuração

### appsettings.json (.NET)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=dbEnviromentMonitoringSystem.db"
  },
  "UrlCallBack": "https://localhost:5003/api/events",
  "UrlApiIot": "http://localhost:5000",
  "UrlFront": "http://localhost:4200"
}
```

### environment.ts (Angular)

```typescript
export const environment = {
  apiUrl: 'https://localhost:5003/api'
};
```

## Como rodar

### Backend (.NET)

```bash
cd backend/EnvironmentMonitoringSystem.API
dotnet run
```

A API estará disponível em https://localhost:5003
Swagger disponível em /swagger para testar endpoints.

### Frontend (Angular)

```bash
cd frontend
npm install
ng serve -o
```

Frontend disponível em http://localhost:4200.


## Endpoints 

| Método | Endpoint          | Descrição                              |
| ------ | ----------------- | -------------------------------------- |
| POST   | /api/devices      | Cadastra dispositivo e integra com IoT |
| GET    | /api/devices      | Lista dispositivos                     |
| GET    | /api/devices/{id} | Detalhes do dispositivo                |
| PUT    | /api/devices/{id} | Atualiza dispositivo                   |
| DELETE | /api/devices/{id} | Remove dispositivo e desregistra IoT   |
| POST   | /api/events       | Recebe eventos do simulador IoT        |

## Observações

Eventos com isAlarm: true são destacados no frontend.
Integração com API Mock de IoT é essencial para registrar e desregistrar dispositivos.
Banco de dados SQLite é utilizado para simplicidade e portabilidade. 
