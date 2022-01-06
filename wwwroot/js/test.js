const app = Vue.createApp({
    data() {
        return {
            values: [
                {
                    Header1: "Value1",
                    Header2: "Value2",
                    Header3: "Value3",
                    Header4: 0
                },
                {
                    Header1: "Value1",
                    Header2: "Value2",
                    Header3: "Value3",
                    Header4: 0
                },
                {
                    Header1: "Value1",
                    Header2: "Value2",
                    Header3: "Value3",
                    Header4: 0
                }
            ],
            Header4: ["HValue1", "HValue2", "HValue3"]
        }
    },
    methods: {
        make_value() {
            return {
                Header1: "",
                Header2: "",
                Header3: "",
                Header4: null
            }
        }
    },
    mounted() {
        console.log(this.values[0])
        axios.get("https://api.randomdatatools.ru/?count=10&params=LastName,FirstName,FatherName,Phone,Login,Password,Email")
            .then(resp => this.values = resp.data)
            .catch(error => console.log(error))
    }
})

app.component("test-table", {
    props: [],
    methods: {
        click() {
            console.log(this.$root.values)
        },
        add_row() {
            this.$root.values.push(this.$root.make_value())
        },
        delete(id) {
            this.$root.values.splice(id, 1)
        }
    },
    template: `
          <div class="table">
              <p class="title">Table</p>
              <table>
                <tr>
                  <th v-for="(value, name) in this.$root.values[0]">{{name}}</th>
                </tr>
                <tr v-for="(row, index) in this.$root.values">
                  <td v-for="(item, name) in this.$root.values[index]">
                    <input v-if="name !== 'Header4'" type="text" v-model="this.$root.values[index][name]" :key="name">
                    <select v-if="name === 'Header4'" type="text" v-model="this.$root.values[index][name]" :key="name">
                      <option disabled value="">Выберите один из вариантов</option>
                      <option v-for="(h_name, h_index) in this.$root.Header4"  :value="h_index">{{h_name}}</option>
                    </select>
                  </td>
                  <td>
                    <button type="button" @click="this.delete(index)">Delete</button>
                  </td>
                </tr>
              </table>
              <button type="submit" @click="click">Submit</button>
              <button type="button" @click="add_row">Add row</button>
          </div>
    `
})

app.mount("#app")