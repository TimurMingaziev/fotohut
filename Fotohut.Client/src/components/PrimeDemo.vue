<template>
  <div class="card">
    <h2>PrimeVue Demo</h2>
    
    <!-- Button Demo -->
    <div class="mb-4">
      <h3>Buttons</h3>
      <Button label="Primary" class="mr-2" />
      <Button label="Secondary" severity="secondary" class="mr-2" />
      <Button label="Success" severity="success" class="mr-2" />
      <Button label="Warning" severity="warning" class="mr-2" />
      <Button label="Danger" severity="danger" />
    </div>

    <!-- Input Demo -->
    <div class="mb-4">
      <h3>Input</h3>
      <InputText v-model="inputValue" placeholder="Enter text" class="mr-2" />
      <span>Value: {{ inputValue }}</span>
    </div>

    <!-- Dropdown Demo -->
    <div class="mb-4">
      <h3>Dropdown</h3>
      <Dropdown
        v-model="selectedCity"
        :options="cities"
        optionLabel="name"
        placeholder="Select a City"
        class="w-full md:w-14rem"
      />
    </div>

    <!-- DataTable Demo -->
    <div class="mb-4">
      <h3>DataTable</h3>
      <DataTable :value="products" tableStyle="min-width: 50rem">
        <Column field="code" header="Code"></Column>
        <Column field="name" header="Name"></Column>
        <Column field="category" header="Category"></Column>
        <Column field="quantity" header="Quantity"></Column>
      </DataTable>
    </div>

    <!-- Card Demo -->
    <div class="mb-4">
      <h3>Card</h3>
      <Card style="width: 25rem">
        <template #header>
          <img alt="User" src="https://primefaces.org/cdn/primevue/images/usercard.png" />
        </template>
        <template #title> Advanced Card </template>
        <template #subtitle> Card Subtitle </template>
        <template #content>
          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Inventore sed consequuntur error repudiandae numquam deserunt quisquam repellat libero asperiores earum nam nobis, culpa ratione quam perferendis esse, cupiditate neque quas!</p>
        </template>
        <template #footer>
          <Button icon="pi pi-check" label="Save" />
          <Button icon="pi pi-times" label="Cancel" class="p-button-secondary ml-2" />
        </template>
      </Card>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Card from 'primevue/card'

const inputValue = ref('')
const selectedCity = ref(null)
const cities = ref([
  { name: 'New York', code: 'NY' },
  { name: 'London', code: 'LDN' },
  { name: 'Paris', code: 'PRS' },
  { name: 'Tokyo', code: 'TKY' }
])

const products = ref([
  {
    id: '1000',
    code: 'f230fh0g3',
    name: 'Bamboo Watch',
    category: 'Accessories',
    quantity: 24
  },
  {
    id: '1001',
    code: 'nvklal433',
    name: 'Black Watch',
    category: 'Accessories',
    quantity: 61
  },
  {
    id: '1002',
    code: 'zz21cz3c1',
    name: 'Blue Band',
    category: 'Fitness',
    quantity: 2
  },
  {
    id: '1003',
    code: '244wgj2ks',
    name: 'Blue T-Shirt',
    category: 'Clothing',
    quantity: 25
  }
])
</script>

<style scoped>
.card {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

h2 {
  margin-bottom: 2rem;
  color: var(--primary-color);
}

h3 {
  margin-bottom: 1rem;
  color: var(--text-color-secondary);
}

.mb-4 {
  margin-bottom: 2rem;
}

.mr-2 {
  margin-right: 0.5rem;
}
</style> 