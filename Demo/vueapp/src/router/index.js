import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import ParisView from "../views/ParisView.vue";
import CairoView from "../views/CairoView.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/Amsterdam",
    name: "Amsterdam",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/AmsterdamView.vue"),
  },
  {
    path: "/Paris",
    name: "Paris",
    component: ParisView,
  },
  {
    path: "/Cairo",
    name: "Cairo",
    component: CairoView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
