﻿<template>
	<v-container fluid grid-list-xl class="mainView">
		<v-file-input
			label="File input"
			class="fileInputWidth"
			@change="previewFiles"
		></v-file-input>
		<div v-if="chosenFileWithPromoterList !== ''">
			<v-row justify="center">
				<v-col cols="12">
                    <v-data-table :headers="headers"
                                  :items="items"
                                  class="whiteBackground">
                        <template v-slot:header.name="{ header }">
                            <span>{{ header.text }}</span>
                        </template>
                        <template v-slot:header.laboratory="{ header }">
                            <span>{{ header.text }}</span>
                        </template>
                        <template v-slot:header.bachelorTopics="{ header }">
                            <span>{{ header.text }}</span>
                        </template>
                        <template v-slot:header.masterTopics="{ header }">
                            <span>{{ header.text }}</span>
                        </template>

                        <template v-slot:item.name="{ item }">
                            <span>{{ item.title }}</span>
                        </template>
                        <template v-slot:item.laboratory="{ item }">
                            <span>{{ item.laboratory }}</span>
                        </template>
                        <template v-slot:item.masterTopics="{ item }">
                            <span>{{ item.masterTopics }}</span>
                        </template>
                        <template v-slot:item.bachelorTopics="{ item }">
                            <span>{{ item.bachelorTopics }}</span>
                        </template>
                    </v-data-table>
				</v-col>
			</v-row>
			<div class="biggerFont">
				<span>Set default value of expected number of topics</span>
				<div class="ml-3">
					<v-text-field
						label="Bachelor"
						class="inputMaxNumber"
					></v-text-field
					><v-btn class="topMargin10">Set</v-btn>
				</div>
				<div class="ml-3 clearLeft">
					<v-text-field
						label="Master"
						class="inputMaxNumber"
					></v-text-field
					><v-btn class="topMargin10">Set</v-btn>
				</div>
			</div>
		</div>
	</v-container>
</template>
<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';

@Component
export default class CardsView extends Vue {
    public chosenFileWithPromoterList = '';
    public data() {
        return {
            headers: [
                {
                    sortable: false,
                    text: 'Name',
                    value: 'name',
                    width: '30%',
                    align: 'center',
                },
                {
                    sortable: false,
                    text: 'Laboratory',
                    value: 'laboratory',
                    width: '20%',
                    align: 'center',
                },
                {
                    sortable: false,
                    text: 'Bachelor topics',
                    value: 'bachelorTopics',
                    width: '20%',
                    align: 'center',
                },
                {
                    sortable: false,
                    text: 'Master Topics',
                    value: 'masterTopics',
                    width: '20%',
                    align: 'center',
                },
                {},
            ],
            items: [
                {
                    title: 'Dakota Rice',
                    laboratory: 'Z1',
                    bachelorTopics: 3,
                    masterTopics: 4,
                },
                {
                    title: 'Dakota Rice',
                    laboratory: 'Z1',
                    bachelorTopics: 3,
                    masterTopics: 4,
                },
                {
                    title: 'Dakota Rice',
                    laboratory: 'Z1',
                    bachelorTopics: 3,
                    masterTopics: 4,
                },
                {
                    title: 'Dakota Rice',
                    laboratory: 'Z1',
                    bachelorTopics: 3,
                    masterTopics: 4,
                },
            ],
        };
    }
    public previewFiles(event): void {
        this.chosenFileWithPromoterList = event.name;
    }
}
</script>
<style lang="scss" scoped>
.mainView {
	width: calc(100% - 370px);
	margin-left: 350px;
	margin-right: 10px;
	margin-top: 0px;
	margin-bottom: 140px;
	background-color: #ffffff;
}

.paintData {
	width: 100%;
	height: 100%;
	border-radius: 0;
}
.inputMaxNumber {
	width: 120px;
	float: left;
}
th span {
	font-size: 30px;
	color: rgb(18, 98, 141);
}
td span {
	font-size: 24px;
	font-weight: bold;
}
.topMargin10 {
	margin-top: 10px;
}
.biggerFont {
	font-size: 24px;
	font-weight: bold;
}
.clearLeft {
	clear: left;
}
.fileInputWidth {
	width: 60%;
}
</style>
