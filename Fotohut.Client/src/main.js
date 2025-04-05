import './assets/main.css'

import { createApp } from 'vue'
import PrimeVue from 'primevue/config'
import App from './App.vue'

// Import PrimeVue styles
import 'primevue/resources/themes/lara-light-blue/theme.css' // theme
import 'primevue/resources/primevue.min.css' // core css
import 'primeicons/primeicons.css' // icons
import 'primeflex/primeflex.css' // primeflex

// PrimeVue
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Card from 'primevue/card'

const app = createApp(App)

// Register PrimeVue Components
app.use(PrimeVue, { ripple: true })
app.component('Button', Button)
app.component('InputText', InputText)
app.component('Dropdown', Dropdown)
app.component('DataTable', DataTable)
app.component('Column', Column)
app.component('Card', Card)

app.mount('#app')
