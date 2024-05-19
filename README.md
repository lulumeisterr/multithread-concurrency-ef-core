# multithread-concurrency-ef-core
Teste de concorrencia, instanciando algumas tasks/threads para realizar operações ao mesmo tempo com objetivo de provocar deadlocks
# Utilização de transação Repeatable Read.
  como camada adicional de garantia de consistência aos dados. 
  pois nenhuma outra transação pode modificar ou inserir novos dados na linha 
  até que a transação seja concluída evitando "phantom reads"/leituras fantasmas

Importante.
  por padrão o beginTransaction ja executa o (READ COMMITTED). Isso ainda garante que as linhas lidas permaneçam consistentes até o final da transação, 
  mesmo que outras transações estejam ocorrendo simultaneamente sem a necessidade também de usar o updlock
  
# UPDLOCK
  realiza bloqueio de atualização nas linhas afetadas pela instrução, o que impede que outras transações modifiquem 
  ou insiram novos dados nessas linhas até que a transação atual seja concluída.

#Tecnologias
 - C# 8.0.204
 - EFCORE : 7.0.0
 - SQL SEVER : 2022


![image](https://github.com/lulumeisterr/multithread-concurrency-ef-core/assets/25963928/93fa6021-8510-489d-863d-840180778421)

