import {data_table} from "./table.js";

const app = Vue.createApp({
    data() {
        return {
            values: [],
            headers: {
                "id": { name: "ID", type: "text" },
                "login": { name: "Логин", type: "text" },
                "hashPassword": { name: "{Хэш пароля}", type: "text" },
                "roleId": { name: "Роль", type: "text" },
                "firstName": { name: "Имя", type: "text" },
                "middleName": { name: "Отчество", type: "text" },
                "lastName": { name: "Фамилия", type: "text" },
                "phoneNumber": { name: "Телефон", type: "tel" },
                "dateOfBirth": { name: "Дата рождения", type: "date" }
            },
            select_data: {
                "roleId": {
                    "1":"Администратор",
                    "2":"Клиент",
                    "3":"Тренер"
                }
            },
            update_url: "/api/user/update-admins"
        }
    },
    mounted() {

        document.title = "Администраторы - АГУ СПОРТ"
        axios
            .get("/api/user/get-users?role=admin")
            .then(resp => {
                this.values = resp.data
            })
            .catch(error => console.log(error))

    }
})

app.component("admins-table-page", {
    components: {
        "data-table": data_table,
    },
    props: [],
    template: `
        <div class="container">
            <data-table title="Администраторы" :select_data="this.$root.select_data" :values="this.$root.values" :headers="this.$root.headers" :update_url="this.$root.update_url"></data-table>
        </div>
    `
})

app.mount("#app")