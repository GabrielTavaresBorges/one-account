<template>
  <div class="footer-watermark-brand">
    YΛΞΛY Software - 2026
  </div>
</template>

<style scoped>
  .footer-watermark-brand {
    position: fixed; /* <— melhor que absolute para global */
    left: 0;
    bottom: 16px;
    width: 100%;
    text-align: center;
    font-size: 0.75rem;
    color: #5D4037;
    opacity: 0.7;
    z-index: 10;
    pointer-events: none; /* não atrapalha clique */
  }
</style>
