<script setup>
// Your component imports will go here
</script>

<template>
  <div id="app">
    <header>
      <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />
      <h1>Fotohut</h1>
    </header>

    <main>
      <div class="card">
        <h2>Welcome to Fotohut</h2>
        <p>Your photo management application</p>
      </div>
    </main>
  </div>
</template>

<style>
#app {
  font-family: var(--font-family);
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: var(--text-color);
  background-color: var(--surface-ground);
  min-height: 100vh;
  padding: 2rem;
}

header {
  line-height: 1.5;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 2rem;
}

.logo {
  display: block;
  margin: 0 auto 1rem;
}

h1 {
  font-size: 1.75rem;
  color: var(--primary-color);
}

.card {
  background: var(--surface-card);
  padding: 2rem;
  border-radius: var(--border-radius);
  max-width: 800px;
  margin: 0 auto;
  box-shadow: var(--card-shadow);
}

@media (min-width: 1024px) {
  header {
    flex-direction: row;
    justify-content: center;
    gap: 1rem;
  }

  .logo {
    margin: 0;
  }
}
</style>
