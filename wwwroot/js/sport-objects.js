const app = Vue.createApp({})

app.component("sport-objects-table", {
    data() {
        return {
            values: [],
            copy: "",
            headers: {
                id: "ID",
                name: "Имя",
                capacity: "Вместимость",
                location: "Расположение",
                startingTime: "Начало",
                closingTime: "Конец"
            }
        }
    },
    mounted() {
        axios.get("https://localhost:5001/api/sport-object/get-objects-with-ids")
            .then(resp => {
                this.values = resp.data
                this.copy = JSON.stringify(resp.data)
            })
            .catch(error => console.log(error))
        console.log(this.copy)
    },
    props: [],
    methods: {
        make_value() {
            let value = {}
            for (let key in this.values[0]) {
                value[key] = null
            }
            return value
        },
        submit() {
            console.log(this.values)
            this.copy = JSON.stringify(this.values)
            axios
                .post("https://localhost:5001/api/sport-object/update-sport-objects", this.values)
                .then(response => console.log(response.data))
                .catch(error => console.log(error))
        },
        add_row() {
            this.values.push(this.make_value())
        },
        delete(id) {
            this.values.splice(id, 1)
        },
        stash() {
            console.log(this.copy)
            this.values = JSON.parse(this.copy)
        }
    },
    template: `
      <div class="table">
      <h1 class="title">Table</h1>
      <table>
        <tr>
          <th v-for="(value, name) in this.values[0]">{{ this.headers[name] ?? name }}</th>
        </tr>
        <tr v-for="(row, index) in this.values">
          <td v-for="(item, name) in this.values[index]">
            <input type="text" v-model="this.values[index][name]" :key="name">
<!--            <select v-if="name === 'Gender'" type="text" v-model="this.values[index][name]" :key="name">
&lt;!&ndash;              <option disabled value="">Выберите</option>&ndash;&gt;
              <option v-for="(h_name, h_key) in this.gender_values"  :value="h_key">{{h_name}}</option>
            </select>-->
          </td>
          <td>
            <button class="delete" type="button" @click="this.delete(index)">
              <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24"><path d="M21.5 6a1 1 0 0 1-.883.993L20.5 7h-.845l-1.231 12.52A2.75 2.75 0 0 1 15.687 22H8.313a2.75 2.75 0 0 1-2.737-2.48L4.345 7H3.5a1 1 0 0 1 0-2h5a3.5 3.5 0 1 1 7 0h5a1 1 0 0 1 1 1zm-7.25 3.25a.75.75 0 0 0-.743.648L13.5 10v7l.007.102a.75.75 0 0 0 1.486 0L15 17v-7l-.007-.102a.75.75 0 0 0-.743-.648zm-4.5 0a.75.75 0 0 0-.743.648L9 10v7l.007.102a.75.75 0 0 0 1.486 0L10.5 17v-7l-.007-.102a.75.75 0 0 0-.743-.648zM12 3.5A1.5 1.5 0 0 0 10.5 5h3A1.5 1.5 0 0 0 12 3.5z"/></svg>
            </button>
          </td>
        </tr>
      </table>
      <div class="table-controls">
        <button class="bottom-button add-row" type="button" @click="add_row">Добавить</button>
        <button class="bottom-button stash" type="button" @click="stash">Отменить изменения</button>
        <button class="bottom-button submit" type="submit" @click="submit">Сохранить</button>
      </div>
      </div>
    `
})

app.mount("#app")