def tituloA(volume):
    altura = int(input(f"Digite a ALTURA do volume {volume}: "))
    return altura 

def tituloL(volume):
    largura = int(input(f"Digite a LARGURA do volume {volume}: "))
    return largura

def tituloC(volume):
   comprimento = int(input(f"Digite o COMPRIMENTO do volume {volume}: "))
   return comprimento

def tituloP(volume):
    peso = int(input(f"Digite o  PESO do volume {volume}: "))
    return peso

def cubagem(altura , largura , comprimento , peso,volume):
    if peso < 300:
        calculo = (altura * largura * comprimento) / 300
        return calculo
    else:
        calculo =  (altura * largura * comprimento) / peso  
        

    print(f"O Valor da cubagem do volume {volume} é {calculo:.2f}\n") 
    return calculo 

def orcamento_por_volume(volume,cubagem, km, taxa = 0.60):
    valor = cubagem * taxa + (1.0 * km )
    print(f"O Valor do volume {volume} é R$ {valor:.2f}\n") 
    return valor

