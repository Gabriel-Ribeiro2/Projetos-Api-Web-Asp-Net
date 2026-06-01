<template>
  <div>
    <h1>Registrar Monster</h1>
    <div class="formulario">
      <div class="formulario-grupo">
        <label>Nome: </label>
        <input v-model="nomeMonster" placeholder="Digite o nome do monster" />
      </div>

      <div class="formulario-grupo">
        <label>Descrição: </label>
        <textarea
          v-model="descricaoMonster"
          placeholder="Digite a descrição do monster"
        />
      </div>

      <div class="formulario-grupo">
        <label>Nota do Pietro: </label>
        <input
          v-model="notaPietroMonster"
          type="number"
          placeholder="Digite a nota do Pietro"
        />
      </div>

      <div class="formulario-grupo">
        <label>Zero Açucar: </label>
        <input
          v-model="zeroMonster"
          type="checkbox"
          placeholder="com ou sem açucar"
        />
      </div>

      <button @click="adicionarMonster"><span>Adicionar Monster</span></button>
      <ul>
        <li v-for="item in monsters" :key="item.id">
          Nome: {{ item.name }} - Descrição: {{ item.descricao }} - Nota do
          Pietro: {{ item.notaPietro }} - Energético é zero açúcar:
          {{ item.zeroAcucar }} - Cancelado: {{ item.cancelado }}
          <button @click="prepararEdicao(item)" title="Editar Monster">
            Editar Monster
          </button>
          <button @click="alterarCancelado(item)" title="Editar Monster">
            Alterar Cancelado
          </button>
          <button @click="deletarMonster(item.id)" title="Editar Monster">
            Deletar
          </button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
const monsters = ref([]);
const nomeMonster = ref("");
const descricaoMonster = ref("");
const notaPietroMonster = ref(0);
const zeroMonster = ref(false);
const idMonsterEmEdicao = ref(null);

const API_URL = "http://localhost:5026/api/Monster";

const buscarMonsters = async () => {
  try {
    const resposta = await axios.get(API_URL);
    monsters.value = resposta.data;
    console.log(monsters.value);
  } catch (erro) {
    console.error("Erro ao buscar monsters");
  }
};

const adicionarMonster = async () => {
  if (!nomeMonster.value || !descricaoMonster.value) {
    alert("Nome e descrição são obrigatórios!");
    return;
  }
  const dadosMonster = {
    name: nomeMonster.value,
    descricao: descricaoMonster.value,
    notaPietro: notaPietroMonster.value,
    zero: zeroMonster.value,
    cancelado: false,
  };

  try {
    if (idMonsterEmEdicao.value) {
      // Se tem ID guardado, fazemos um PUT para atualizar a tarefa toda
      await axios.put(`${API_URL}/${idMonsterEmEdicao.value}`, dadosMonster);
      idMonsterEmEdicao.value = null;
    } else {
      // Se não tem ID, é uma tarefa nova (POST)
      await axios.post(API_URL, dadosMonster);
    }

    nomeMonster.value = "";
    descricaoMonster.value = "";
    notaPietroMonster.value = 0;
    zeroMonster.value = false;
    buscarMonsters();
  } catch (erro) {
    console.error("Erro ao salvar monster", erro);
  }
};
onMounted(buscarMonsters);

const prepararEdicao = (item) => {
  nomeMonster.value = item.name;
  descricaoMonster.value = item.descricao;
  notaPietroMonster.value = item.notaPietro;
  zeroMonster.value = item.zeroAcucar;
  idMonsterEmEdicao.value = item.id;
};

// Deletar Tarefa
const deletarMonster = async (id) => {
  if (confirm("Tem certeza que deseja excluir permanentemente esta tarefa"))
    try {
      await axios.delete(`${API_URL}/${id}`);
      buscarMonsters();
    } catch (erro) {
      console.error("Erro ao deletar monster", erro);
    }
};

const alterarCancelado = async (item) => {
  try {
    // Inverte o valor atual do booleano cancelado
    // se está true vira false e vice-versa
    const novoStatus = !item.cancelado;

    // Faz o PATCH enviando o ID na URL e o booleano puro no corpo
    await axios.patch(`${API_URL}/${item.id}/status`, novoStatus, {
      headers: { "Content-Type": "application/json" },
    });

    buscarMonsters();
  } catch (erro) {
    console.error("Erro ao altenar status:", erro);
  }
};
</script>
