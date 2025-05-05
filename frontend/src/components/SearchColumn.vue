<script setup>
const props = defineProps({
    provider: String,
    searchQuery: {
        type: String,
        required: true,
    }
});
const emit = defineEmits(['update-provider', 'update:searchQuery'])

function setProvider(provider) {
    emit('update-provider', provider);
}

function updateQuery(query) {
  emit('update:searchQuery', query)
}
</script>

<template>
  <div class="menu-column">

    <!-- Search Bar -->
    <div class="search-bar">
      <span class="search-icon">🔍</span>
    
      <input
        type="text"
        :value="props.searchQuery"
        @input="updateQuery($event.target.value)"
        placeholder="Search task"
        class="search-input"
      />
    </div>

    <!-- Provider Selection -->
    <p class="section">Provider Options</p>

    <div
      @click="setProvider('json')"
      class="provider-button"
      :class="{ selected: provider === 'json' }"
    >
        JSON Provider
    </div>

    <div
      @click="setProvider('sqlite')"
      class="provider-button"
      :class="{ selected: provider === 'sqlite' }"
    >
        SQLite Provider
</div>

  </div>
</template>
  
<style scoped>
.menu-column {
  background: #f2f2f2;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.menu-column > * {
    background-color: transparent;
}

.search-bar {
  display: flex;
  margin-top: 40px;
  width: 90%;
  background: white;
  border-radius: 8px;
  padding: 4px 8px;
}

.search-icon {
  margin-right: 8px;
}

.search-input {
  width: 90%;
  color: black;
  font-size: 14px;
  border: none;
  outline: none;
}

.section{
  margin-top: 20px;
  padding: 0 5px;
  width: 90%;
  font-size: 10px;;
  color: gray;
  text-align: left;
}

.provider-button {
  margin-top: 5px;
  padding: 5px;
  min-height: 30px;
  width: 90%;
  color: #505050;
  text-align: left;
  border: 0;
  border-radius: 5px;
  display: flex;
  justify-content: space-between;
}

.provider-button:hover {
  background-color: #0000000d;
}

.provider-button.selected {
 background-color: #0000000d;
}

.divider {
  width: 90%;
  height: 1px;
  background-color: rgba(0, 0, 0, 0.2);
  margin: 1% auto;
}
</style>