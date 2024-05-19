# multithread-concurrency-ef-core
Teste de concorrencia, instanciando algumas tasks/threads para realizar operações ao mesmo tempo com objetivo de provocar deadlocks
# Utilização de transação Repeatable Read.
  como camada adicional de garantia de consistência aos dados. 
  pois nenhuma outra transação pode modificar ou inserir novos dados na linha 
  até que a transação seja concluída evitando "phantom reads"/leituras fantasmas


# UPDLOCK
  realiza bloqueio de atualização nas linhas afetadas pela instrução, o que impede que outras transações modifiquem 
  ou insiram novos dados nessas linhas até que a transação atual seja concluída.
