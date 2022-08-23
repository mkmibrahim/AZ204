const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
});
module.exports = {
  devServer: {
    // proxy: "http://localhost:5000"
    //proxy: process.env.BACKEND_API
    proxy: "https://az204demobackendapp123.azurewebsites.net"
    }
}