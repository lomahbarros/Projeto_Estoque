import definicao



km = int(input("Digite o KM rodado")) 
valor_da_nf = 150,00

volumes = int(input("Digite a quantidade de volumes"))

lista_volumes = []

for volume in range(1,volumes+1):

    altura = definicao.tituloA(volume)
    
    largura = definicao.tituloL(volume)
    
    comprimento = definicao.tituloC(volume)
    
    peso = definicao.tituloP(volume)

    cubagem = definicao.cubagem(altura,largura,comprimento,peso,volume)

    valor_por_caixa = definicao.orcamento_por_volume(volume,cubagem,km)

    dados_da_caixa = {
        "volume": volume,
        "altura": altura,
        "largura": largura,
        "comprimento": comprimento,
        "peso": peso,
        "Cubagem": cubagem
    }

    lista_volumes.append(dados_da_caixa)

    print(lista_volumes)
